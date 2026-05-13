using MIF2.UI;
using MIF2.Controllers;
using System;
using System.Windows.Forms;

namespace MIF2
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MapForm mapForm = new MapForm();
            Controller controller = new Controller();
            controller.SetMapForm(mapForm);

            Application.Run(mapForm);
        }
    }
}


// Баги
/*
 1) вылет при перполнениии очереди команд при смене алгоритма цвета
 2) зависание при смене алгоритма цвета на поучзе из-за системы противодействоания багу 1


 3) начало симуляции, остановка симуляции, запуск. Симуляция не сбрасывается, а продолжатся с смомена отановки
 
 
 */
