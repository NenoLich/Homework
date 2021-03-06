﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Homework
{
    /// <summary>
    /// Главное меню
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly Timer _timer = new Timer();
        private object[] _parameters;
        private MethodInfo _method;

        public MainForm()
        {
            InitializeComponent();

            var homeworks = Assembly.GetExecutingAssembly().GetTypes()
                .Where((x) => x.BaseType == typeof(Homework))
                .OrderBy((x) => x.Name).ToArray().ToList();


            homeworks_comboBox.Items.AddRange(homeworks.ToArray());
            homeworks_comboBox.SelectedIndex = homeworks_comboBox.Items.Count - 1;

            _timer.Tick += Time_tick;
        }

        /// <summary>
        /// Выход из программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Переход к следующему этапу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void next_Button_Click(object sender, EventArgs e)
        {
            method_panel.Enabled = true;
            method_panel.Visible = true;
            homework_panel.Enabled = false;

            //Создаем экземпляр класса, соответсвующего выбранному домашнему заданию
            Type selectedClass = (Type)homeworks_comboBox.SelectedItem;
            //Находим методы из выбранного класса(только открытые и статические)
            MethodInfo[] methods = selectedClass?.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            if (methods != null) method_comboBox.Items.AddRange(methods);
        }

        #region Method panel

        /// <summary>
        /// Возврат к предыдущему этапу выбора ДЗ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void method_back_Button_Click(object sender, EventArgs e)
        {
            homework_panel.Enabled = true;
            method_panel.Enabled = false;
            method_comboBox.Items.Clear();
        }

        /// <summary>
        /// Попытка вызова метода, соответсвующего ДЗ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void method_next_Button_Click(object sender, EventArgs e)
        {
            if (method_comboBox.SelectedItem == null)
            {
                _timer.Interval = 300;
                _timer.Enabled = true;
                method_label.ForeColor = Color.Red;
                return;
            }

            MethodInvoke();
        }

        /// <summary>
        /// Проверка на наличие параметров выбранного метода
        /// </summary>
        private void MethodInvoke()
        {
            _method = (MethodInfo)method_comboBox.SelectedItem;
            ParameterInfo[] parametersInfo = _method.GetParameters();
            if (parametersInfo?.Length != 0)
            {
                parameters_panel.Enabled = true;
                parameters_panel.Visible = true;
                method_panel.Enabled = false;

                parameters_comboBox.Items.AddRange(parametersInfo);
                parameters_comboBox.SelectedIndex = 0;
                _parameters = new object[parametersInfo.Length];
            }
            else
            {
                _parameters = null;

                Execution();
            }
        }

        /// <summary>
        /// Создание экземпляра класса из иерархии Homework и вызов метода
        /// </summary>
        private void Execution()
        {
            Homework homework = (Homework)Activator.CreateInstance((Type)homeworks_comboBox.SelectedItem);
            homework.ExecuteHomework(_method, _parameters);
        }

        private void Time_tick(object sender, EventArgs e)
        {
            method_label.ForeColor = DefaultForeColor;
        }

        #endregion

        #region Parameters panel

        /// <summary>
        /// Считывание введенного параметра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void apply_Button_Click(object sender, EventArgs e)
        {
            if (Utility.TryRead(parameters_textBox.Text, out _parameters[parameters_comboBox.SelectedIndex], Utility.TryReadConditions.WithoutConsoleRequest))
            {
                if (parameters_comboBox.SelectedIndex == parameters_comboBox.Items.Count - 1)
                {
                    parameters_comboBox.SelectedIndex = 0;
                    parameters_next_button.Focus();
                }
                else
                {
                    parameters_comboBox.SelectedIndex++;
                }
            }

            parameters_textBox.Focus();
        }

        /// <summary>
        /// Переход к выполнению метода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void parameters_next_button_Click(object sender, EventArgs e)
        {
            Execution();
        }

        /// <summary>
        /// Возврат к предыдущему этапу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void parameters_back_button_Click(object sender, EventArgs e)
        {
            method_panel.Enabled = true;
            parameters_panel.Enabled = false;
        }

        /// <summary>
        /// Очистка поля для другого параметра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void parameters_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            parameters_textBox.Clear();
        }

        #endregion

        /// <summary>
        /// Сокрытие консоли
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Activated(object sender, EventArgs e)
        {
            Utility.HideConsole();
        }
    }
}
