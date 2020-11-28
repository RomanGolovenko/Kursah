using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Datagrind
{
    public partial class Form3 : Form
    {

        static string connStr = "server = osp74.ru; user = st_2_1;database = st_2_1;password = 33674763; port =33333";
       

        MySqlConnection connection_db = new MySqlConnection(connStr);

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

            //Получаем ID категории из глобального класса
            string id_user = edit_usr.id;
            //Тестируем успешность передачи, после подтверждения факта передачи, можно закоментить эту строку
            MessageBox.Show(id_user);
            //Формируем SQL запрос на выборку определённой строки
            string sql_current_categ = "SELECT * FROM t_categ WHERE id_categ=" + id_user;
            MySqlCommand current_user_command = new MySqlCommand(sql_current_categ, connection_db);
            connection_db.Open();
            MySqlDataReader current_user_reader = current_user_command.ExecuteReader();

            //Получаем текущие значения полей пользователя
            while (current_user_reader.Read())
            {
                textBox2.Text = current_user_reader[1].ToString();
               
               
                //Изменение метки формы
                this.Text = "Редактирование пользователя: " + current_user_reader[1];
            }
            connection_db.Close();
        }


        // Кнопка сохранения 
        private void button1_Click(object sender, EventArgs e)
        {
            //Новые параметры из полей формы
            string new_value = textBox2.Text;


            if (textBox2.Text.Length > 0)

            {
                //Формируем строку запроса на добавление строк
                string sql_update_user = "UPDATE t_categ SET " +
                    "title_categ = '" + new_value + "' " +
                    "WHERE id_categ = " + edit_usr.id;

                //Посылаем запрос на обновление данных
                MySqlCommand update_user = new MySqlCommand(sql_update_user, connection_db);
                try
                {
                    connection_db.Open();
                    update_user.ExecuteNonQuery();
                    MessageBox.Show("Изменение прошло успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка изменения строки \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                finally
                {
                    connection_db.Close();
                }
            }
            else
            {
                MessageBox.Show("ФИО пользователя не должно быть пустым", "Информация");
            }

            



        }

       

    }


}



