using Dapper;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using Quiz_using_Dapper.Classes;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Quiz_using_Dapper
{
    public partial class Form1 : Form

    {
        private readonly string conn = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        public Form1()
        {
            InitializeComponent();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        #region Get_all Buttons

        private void Get_all_products_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(conn);
            try
            {
                connection.Open();

                string query = "SELECT * FROM Products";
                var products = connection.Query<Products>(query).ToList();

                dataGridView1.DataSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "მოხდა შეცდომა! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(conn);
            try
            {
                connection.Open();

                string query = "SELECT * FROM Users";
                var products = connection.Query<Users>(query).ToList();

                dataGridView1.DataSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "მოხდა შეცდომა! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(conn);
            try
            {
                connection.Open();

                var orders = connection.Query<Order>("GetAllOrders", commandType: CommandType.StoredProcedure).ToList();

                dataGridView1.DataSource = orders;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "მოხდა შეცდომა! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion

        #region Inserts

        #region Insert Into Order with Procedure 
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(conn);

            try
            {
                int userId = Convert.ToInt32(numericUpDown3.Value);
                int productId = Convert.ToInt32(numericUpDown1.Value);
                int quantity = Convert.ToInt32(numericUpDown2.Value);

                connection.Open();

                var dynParameters = new DynamicParameters();
                dynParameters.Add("@UserId", userId, DbType.Int32);
                dynParameters.Add("@ProductId", productId, DbType.Int32);
                dynParameters.Add("@Quantity", quantity, DbType.Int32);

                int rowsAffected = connection.Execute("InsertOrder", dynParameters, commandType: CommandType.StoredProcedure);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Order inserted successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to insert order.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion

        #region Without procedure other inputs
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string email = textBox2.Text;
            string password = textBox3.Text;

            SqlConnection connection = new SqlConnection(conn);
            try
            {


                await connection.OpenAsync();
                string query = @"INSERT INTO Users (Username, Email, Password)
                         VALUES (@Username, @Email, @Password)";

                var dynParameters = new DynamicParameters();
                await Task.Run(() =>
                {
                    dynParameters.Add("@Username", username, DbType.String);
                    dynParameters.Add("@Email", email, DbType.String);
                    dynParameters.Add("@Password", password, DbType.String);
                });

                int rowsAffected = await connection.ExecuteAsync(query, dynParameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("User inserted successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to insert user.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "მოხდა შეცდომა! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }



        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {

        }

        private async void button5_Click(object sender, EventArgs e)
        {
            string name = textBox6.Text;
            string Description = textBox5.Text;
            int price = Convert.ToInt32(numericUpDown4.Value);

            SqlConnection connection = new SqlConnection(conn);
            try
            {
                await connection.OpenAsync();

                string query = @"INSERT INTO Products (ProductName, Description, Price)
                         VALUES (@ProductName, @Description, @Price)";

                var dynParameters = new DynamicParameters();
                await Task.Run(() =>
                {
                    dynParameters.Add("@ProductName", name, DbType.String);
                    dynParameters.Add("@Description", Description, DbType.String);
                    dynParameters.Add("@Price", price, DbType.Decimal);
                });

                int rowsAffected = await connection.ExecuteAsync(query, dynParameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Product inserted successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to insert product.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "მოხდა შეცდომა! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion

        #endregion

        #region Update

        #region Using Transition
        private void numericUpDown9_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown10_ValueChanged(object sender, EventArgs e)
        {

        }

        private async void button7_Click(object sender, EventArgs e)
        {
            int orderId = (int)numericUpDown9.Value;
            int newQuantity = (int)numericUpDown10.Value;
            SqlConnection connection = new SqlConnection(conn);
            await connection.OpenAsync();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string updateOrderQuery = @"UPDATE Orders
                                            SET Quantity = @NewQuantity
                                            WHERE OrderId = @OrderId";

                    var dynParameters1 = new DynamicParameters();
                    await Task.Run(() =>
                    {
                        dynParameters1.Add("@NewQuantity", newQuantity, DbType.Int32);
                        dynParameters1.Add("@OrderId", orderId, DbType.Int32);
                    });
                    connection.Execute(updateOrderQuery, dynParameters1, transaction: transaction);

                    string updateProductQuery = @"UPDATE Products
                                              SET Quantity = Quantity - @Quantity
                                              WHERE ProductId = (SELECT ProductId FROM Orders WHERE OrderId = @OrderId)";

                    var dynParameters2 = new DynamicParameters();
                    await Task.Run(() =>
                    {
                        dynParameters2.Add("@Quantity", newQuantity, DbType.Int32);
                        dynParameters2.Add("@OrderId", orderId, DbType.Int32);
                    });

                    await connection.ExecuteAsync(updateProductQuery, dynParameters2, transaction: transaction);

                    transaction.Commit();

                    MessageBox.Show("Order and product quantity updated successfully!");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Failed to update order and product quantity. Transaction rolled back. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
            }

        }
        #endregion

        #region without transition
        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button6_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(numericUpDown8.Value);
            string username = textBox8.Text;
            string email = textBox7.Text;
            string password = textBox4.Text;

            SqlConnection connection = new SqlConnection(conn);

            try
            {
                await connection.OpenAsync();

                string query = @"UPDATE Users
                 SET Username = @Username, Email = @Email, Password = @Password
                 WHERE UserId = @UserId";

                var dynParameters = new DynamicParameters();
                await Task.Run(() =>
                {
                    dynParameters.Add("@UserId", userId, DbType.Int32);
                    dynParameters.Add("@Username", username, DbType.String);
                    dynParameters.Add("@Email", email, DbType.String);
                    dynParameters.Add("@Password", password, DbType.String);
                });

                int rowsAffected = await connection.ExecuteAsync(query, dynParameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("User updated successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to update user.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion

        #endregion

        #region Deletes
        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {

        }
        private async void button9_Click(object sender, EventArgs e)
        {
            int userIdi = (int)numericUpDown5.Value;
            var users = new List<Users>();
            var orders = new List<Order>();
            var products = new List<Products>();
            try
            {
                SqlConnection connection = new SqlConnection(conn);


                await connection.OpenAsync();

                var deletedUser = (await connection.QueryAsync<Users>(
                    "SELECT * FROM Users")).FirstOrDefault(u => u.UserId == userIdi);

                if (deletedUser != null)
                {
                    var dynParameters = new DynamicParameters();
                    await Task.Run(() =>
                    {
                        dynParameters.Add("@UserId", userIdi, DbType.Int32);
                    });
                    await connection.ExecuteAsync("DELETE FROM Orders WHERE UserId = @UserId", dynParameters);

                    await connection.ExecuteAsync("DELETE FROM Users WHERE UserId = @UserId", dynParameters);


                    MessageBox.Show("User and related records deleted successfully!");
                }
                else
                {
                    MessageBox.Show("User with ID 4 not found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region scalar
        private void button8_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(conn);
            DateTime currentDateTime = connection.ExecuteScalar<DateTime>("SELECT GETDATE()");

            List<DateTime> dateTimeList = new List<DateTime> { currentDateTime };

            dataGridView1.DataSource = dateTimeList;
        }
        #endregion

        #region zedmeti
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }



        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }


        #endregion

    }

}
