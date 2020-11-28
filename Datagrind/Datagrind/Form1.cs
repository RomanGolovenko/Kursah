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
using MySql.Data;

namespace Datagrind
{
    public partial class Form1 : Form
    {
        // Глобальные переменные
                                      // Точка в хода в бд
        string connStr = "server = osp74.ru; user = st_2_1;database = st_2_1;password = 33674763; port =33333";// Строка входа в бд
        MySqlConnection conn_db;
        private MySqlDataAdapter MyDA = new MySqlDataAdapter();
        //Объявление BindingSource, основная его задача, это обеспечить унифицированный доступ к источнику данных.
        private BindingSource bSource = new BindingSource();
        //DataSet - расположенное в оперативной памяти представление данных, обеспечивающее согласованную реляционную программную 
        //модель независимо от источника данных.DataSet представляет полный набор данных, включая таблицы, содержащие, упорядочивающие 
        //и ограничивающие данные, а также связи между таблицами.
        private DataSet ds = new DataSet();
        //Представляет одну таблицу данных в памяти.
        private DataTable table = new DataTable();
        //Переменная для ID записи в БД, выбранной в гриде
        string id_selected_categ;

       

        //Метод получения ID выделенной строки, для последующего вызова его в нужных методах
        public void GetSelectedIDString()
        {
            //Переменная для индекса выбранной строки в гриде
            string index_selected_categ;
            //Индекс выбранной строки
            index_selected_categ = dataGridView1.SelectedCells[0].RowIndex.ToString();
            //ID конкретной записи в Базе данных, на основании индекса строки
            id_selected_categ = dataGridView1.Rows[Convert.ToInt32(index_selected_categ)].Cells[0].Value.ToString();
            //В глобальный класс заносим переменную, которая будет хранить Идентификатор изменяемой категории на другой форме
            edit_usr.id = id_selected_categ;
        }



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Инициализируем соединение с БД
            conn_db = new MySqlConnection(connStr);
            //Вызываем метод для заполнение дата Грида
            GetListUsers();
            //Видимость полей в гриде
            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = true;
           
            //Ширина полей
            dataGridView1.Columns[0].FillWeight = 10;
            dataGridView1.Columns[1].FillWeight = 50;
           
            
            //Растягивание полей грида
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            
            //Убираем заголовки строк
            dataGridView1.RowHeadersVisible = false;
            //Показываем заголовки столбцов
            dataGridView1.ColumnHeadersVisible = true;
        }

        //Метод наполнения DataGreed
        public void GetListUsers()
        {
            //Запрос для вывода строк в БД
            string commandStr = "SELECT id_categ AS 'ID', title_categ AS 'title_categ' FROM t_categ";
            //Открываем соединение
            conn_db.Open();
            //Объявляем команду, которая выполнить запрос в соединении conn
            MyDA.SelectCommand = new MySqlCommand(commandStr, conn_db);
            //Заполняем таблицу записями из БД
            MyDA.Fill(table);
            //Указываем, что источником данных в bindingsource является заполненная выше таблица
            bSource.DataSource = table;
            //Указываем, что источником данных ДатаГрида является bindingsource 
            dataGridView1.DataSource = bSource;
            //Закрываем соединение
            conn_db.Close();
           
        }

        //Метод обновления DataGreed
        public void reload_list()
        {
            //Чистим виртуальную таблицу
            table.Clear();
            //Вызываем метод получения записей, который вновь заполнит таблицу
            GetListUsers();
        }


        //Выделение всей строки по ЛКМ
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
            //dataGridView1.CurrentRow.Selected = true;

            //Метод получения ID выделенной строки в глобальную переменную
            GetSelectedIDString();
        }


        // Кнопка изменить
        private void button1_Click(object sender, EventArgs e)
        {
            GetSelectedIDString();
            Form3 fm3 = new Form3();
          //  fm3.textBox1.Text = this.label1.Text;
            fm3.ShowDialog();

        }

        // Обновление
        private void button2_Click(object sender, EventArgs e)
        {
            reload_list();
        }
    }
}
