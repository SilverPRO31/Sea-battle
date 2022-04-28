using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace Морской_Бой
{
    public partial class Form1 : Form
    {
        int neu=0, a = 0, b = 0, g = 1,po1=0,pr1=0,po2=0,pr2=0;
        private const int MAX_RECURSIVE_CALLS = 1000;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.RowCount = 10;
            dataGridView2.RowCount = 10;
            int i = 0,j=1;
            while (i < 10)
            {
                dataGridView1.Rows[i].HeaderCell.Value = $"{j}";
                dataGridView2.Rows[i].HeaderCell.Value = $"{j}";
                i++;
                j++;
            }
        }
        void defspav(int a, int b, ref int c,int g)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int o = 0; o < 3; o++)
                {
                    if (g == 1)
                    {
                        if (DataBase.array1[a - 1 + i, b - 1 + o] == false)
                        {
                            c++;
                        }
                    }
                    if (g == 2)
                    {
                        if (DataBase.array2[a - 1 + i, b - 1 + o] == false)
                        {
                            c++;
                        }
                    }
                }
            }
        }
        void shestniz(int a,int b, ref int c, ref int d,int kol,int g)
        {
            int z;
            for (int u = 1; u < kol+1; u++)
            {
                z = 0;
                for (int i = 0; i < 2; i++)
                {
                    for (int o = 0; o < 3; o++)
                    {
                        if (g == 1)
                        {
                            if (DataBase.array1[a + u + i, b - 1 + o] == false)
                            {
                                z++;
                            }
                        }
                        if (g == 2)
                        {
                            if (DataBase.array2[a + u + i, b - 1 + o] == false)
                            {
                                z++;
                            }
                        }
                    }
                }
                if (z == 6)
                {
                    c += 1;
                }
            }
            if (c == kol)
            {
                for (int i = 0; i < kol+1; i++)
                {
                    if (g == 1)
                    {
                        dataGridView1.Rows[a - 2 + i].Cells[b - 2].Value = "╳";
                        DataBase.array1[a + i, b] = true;
                        DataBase.bok[a + i, b] = 1;
                    }
                    if (g == 2)
                    {
                        DataBase.array2[a + i, b] = true;
                        DataBase.bok2[a + i, b] = 1;
                    }
                }
                d += 1;
            }

        }
        void shestprav(int a, int b, ref int c, ref int d, int kol,int g)
        {
            int z;
            for (int u = 1; u < kol+1; u++)
            {
                z = 0;
                for (int i = 0; i < 3; i++)
                {
                    for (int o = 0; o < 2; o++)
                    {
                        if (g == 1)
                        {
                            if (DataBase.array1[a - 1 + i, b + u + o] == false)
                            {
                                z++;
                            }
                        }
                        if (g == 2)
                        {
                            if (DataBase.array2[a - 1 + i, b + u + o] == false)
                            {
                                z++;
                            }
                        }
                    }
                }
                if (z == 6)
                {
                    c += 1;
                }
            }
            if (c == kol)
            {
                for (int i = 0; i < kol + 1; i++)
                {
                    if (g == 1)
                    {
                        dataGridView1.Rows[a - 2].Cells[b - 2 + i].Value = "╳";
                        DataBase.array1[a, b + i] = true;
                        DataBase.bok[a, b + i] = 1;
                    }
                    if (g == 2)
                    {
                        DataBase.array2[a, b + i] = true;
                        DataBase.bok2[a, b + i] = 1;
                    }
                }
                d += 1;
            }
        }
        Random r = new Random();
        void spavn( int k,int g, int t)
        {
            int a, b, l, np1, h, w;
            a = 0;
            while (a < k)
            {
                b = 0;
                while (b == 0)
                {
                    h = r.Next(2, 12);
                    w = r.Next(2, 12);
                    l = 0;
                    defspav(h, w, ref l, g);
                    if (l == 9)
                    {

                        np1 = r.Next(0, 2);
                        l = 0;
                        if (np1 == 0)
                        {
                            if (h < 9)
                            {
                                shestniz(h, w, ref l, ref b, t, g);
                            }
                        }
                        if (np1 == 1)
                        {
                            if (w < 9)
                            {
                                shestprav(h, w, ref l, ref b, t, g);
                            }
                        }
                    }
                }
                a++;
            }
        }
        void str(DataGridViewCellEventArgs e,int k)
        {
            int a2, b2,k1, k3, t=0,a1=0,b1=0;
            a2 = e.RowIndex; 
            b2= e.ColumnIndex;
            dataGridView2.Rows[a2].Cells[b2].Style.BackColor = Color.Gray;
            if (DataBase.array2[a2+2, b2+2] == true)
            {
                po2++;
                k1 = 0;
                k3 = 0;
                dataGridView2.Rows[a2].Cells[b2].Value = "╳";
                provpop(a2+2, b2+2, 2, ref k1, ref k3,ref a1,ref b1);
                if (k1 < 2)
                {
                    if (k3 == 0)
                    {
                        obvod(ref a2, ref b2, 2, ref t, ref neu);
                    }
                    if (k3 != 0)
                    {
                        DataBase.bok2[a2 + 2, b2 + 2] = 4;
                        DataBase.bok2[a1, b1] = 4;
                        k1 = 0;
                        dopprov(a1,b1,2,ref k1);
                        if (k1 == 0)
                        {
                            DataBase.bok2[a2 + 2, b2 + 2] = 1;
                            DataBase.bok2[a1, b1] = 3;
                            t = 1;
                            while (t == 1)
                            {
                                t = 0;
                                obvod(ref a2, ref b2, 2, ref t, ref neu);
                            }
                        }
                        if (k1 != 0)
                        {
                            DataBase.bok2[a2 + 2, b2 + 2] = 3;
                            DataBase.bok2[a1, b1] = 3;
                        }
                    }
                }
                if (k1 > 1)
                {
                    DataBase.bok2[a2+2, b2+2] = 3;
                }
                label7.Text = $"{po2}";
                label7.ForeColor = Color.Green;
            }
            if (DataBase.array2[a2+2, b2+2] == false)
            {
                pr2++;
                DataBase.bok2[a2+2, b2+2] = 2;
                if (k == 1)
                {
                    label9.Text = $"{pr2}";
                    label9.ForeColor = Color.Red;
                    label1.Text = "Противник ходит";
                    label1.ForeColor = Color.Red;
                    strpr();

                }
            }
        }
        void strpr()
        {
            var ti = Task.Run(async delegate
            {
                await Task.Delay(1000);
                return 42;
            });
            int p,k1,k3,t=0,a1=0,b1=0;
            p = 1;
            while (p == 1)
            {
                k1 = 0;
                k3 = 0;
                ti.Wait();
                if (neu == 0)
                {
                    a = r.Next(2, 12);
                    b = r.Next(2, 12);
                    if (DataBase.bok[a, b] != 2)
                    {
                        mexanstr(ref a, ref b, ref k1, ref k3, ref a1, ref b1, ref t, ref p, 1, ref neu,1);
                    }
                }
                if ((neu == 1) && (p==1))
                {
                    if (DataBase.bok[a, b] != 2)
                    {
                        mexanstr(ref a, ref b, ref k1, ref k3, ref a1, ref b1, ref t, ref p, 1, ref neu, 2);
                    }
                }
            }
            label1.Text = "Вы ходите";
            label1.ForeColor = Color.Green;
        }
        void mexanstr(ref int a,ref int b,ref int k1,ref int k3, ref int a1, ref int b1, ref int t,ref int p,int q,ref int neu, int po)
        {
            var ti = Task.Run(async delegate
            {
                await Task.Delay(1000);
                return 42;
            });
            k1 = 0;
            k3 = 0;
            if (po==1)
            {
                dataGridView1.Rows[a - 2].Cells[b - 2].Style.BackColor = Color.Gray;
                if (DataBase.array1[a, b] == true)
                {
                    po1++;
                    neu = 1;
                    provpop(a, b, 1, ref k1, ref k3, ref a1, ref b1);
                    if (k1 < 2)
                    {
                        if (k3 == 0)
                        {
                            ti.Wait();
                            obvod(ref a, ref b, 1, ref t, ref neu);
                        }
                        if (k3 != 0)
                        {
                            DataBase.bok[a, b] = 4;
                            DataBase.bok[a1, b1] = 4;
                            k1 = 0;
                            dopprov(a1, b1, 1, ref k1);
                            if (k1 == 0)
                            {
                                DataBase.bok[a, b] = 1;
                                DataBase.bok[a1, b1] = 3;
                                t = 1;
                                ti.Wait();
                                while (t == 1)
                                {
                                    t = 0;
                                    obvod(ref a, ref b, 1, ref t, ref neu);
                                }
                            }
                            if (k1 != 0)
                            {
                                DataBase.bok[a, b] = 3;
                                DataBase.bok[a1, b1] = 3;
                            }
                        }
                    }
                    if (k1 > 1)
                    {
                        DataBase.bok[a, b] = 3;
                        if (q == 1)
                        {
                            if (g == 5)
                            {
                                g = 1;
                            }
                            dopstr(ref a, ref b, ref k1, ref k3, ref a1, ref b1, ref t, ref p,ref g, ref neu);//
                        }
                    }
                    label3.Text = $"{po1}";
                    label3.ForeColor = Color.Green;
                }
                if (DataBase.array1[a, b] == false)
                {
                    pr1++;
                    DataBase.bok[a, b] = 2;
                    p = 0;
                    label5.Text = $"{pr1}";
                    label5.ForeColor = Color.Red;
                }
            }else if (po==2)
            {
                dopstr(ref a, ref b, ref k1, ref k3, ref a1, ref b1, ref t, ref p,ref g, ref neu);//
            }
        }
        void dopstr(ref int a,ref int b, ref int k1, ref int k3, ref int a1, ref int b1, ref int t, ref int p,ref int g,ref int neu)
        {
            int w=0,x=1;
                if ((g == 1) && (p==1))
                {
                    astr(a, b, w,ref p, ref neu, x, k1, k3, a1, b1, t, ref g,2);
                }
                if ((g == 2) && (p == 1))
                {
                    astr(a, b, w, ref p, ref neu, x, k1, k3, a1, b1, t, ref g,1);
                }
                if ((g == 3) && (p == 1))
                {
                    bstr(a, b, w, ref p, ref neu, x, k1, k3, a1, b1, t, ref g,2);
                }
                if ((g == 4) && (p == 1))
                {
                    bstr(a, b, w, ref p, ref neu, x, k1, k3, a1, b1, t, ref g,1);
                }
        }
        void astr(int izm,int neizm,int pogr, ref int p,ref int neu ,int x,int k1,int k3,int a1,int b1,int t,ref int g,int i)
        {
            var ti = Task.Run(async delegate
            {
                await Task.Delay(1000);
                return 42;
            });
            while ((p == 1) && (neu == 1) && (x == 1))
            {
                if (i == 1)
                {
                    izm += 1;
                }
                if (i == 2)
                {
                    izm -= 1;
                }
                pogr++;
                if ((izm - 2) < 10 && (neizm - 2) < 10 && (izm - 2) >= 0 && (neizm - 2) >= 0)
                {
                    if (DataBase.bok[izm, neizm] == 2)
                    {
                        x = 0;
                    }
                    if (DataBase.bok[izm, neizm] == 3)
                    {
                        x = 0;
                    }
                    if (DataBase.bok[izm, neizm] != 2)
                    {
                        ti.Wait();
                        mexanstr(ref izm, ref neizm, ref k1, ref k3, ref a1, ref b1, ref t, ref p, 2, ref neu,1);
                    }
                }
                else
                {
                    x = 0;
                }
            }
            if (i == 1)
            {
                izm -= pogr;
            }
            if (i == 2)
            {
                izm += pogr;
            }
            if (neu == 1)
            {
                g += 1;
            }
            else if (neu != 1)
            {
                g = 1;
            }
        }
        void bstr(int neizm, int izm, int pogr, ref int p, ref int neu, int x, int k1, int k3, int a1, int b1, int t, ref int g, int i)
        {
            var ti = Task.Run(async delegate
            {
                await Task.Delay(1000);
                return 42;
            });
            while ((p == 1) && (neu == 1) && (x == 1))
            {
                if (i == 1)
                {
                    izm += 1;
                }
                if (i == 2)
                {
                    izm -= 1;
                }
                pogr++;
                if ((izm - 2) < 10 && (neizm - 2) < 10 && (izm - 2) >= 0 && (neizm - 2) >= 0)
                {
                    if (DataBase.bok[neizm, izm] == 2)
                    {
                        x = 0;
                    }
                    if (DataBase.bok[neizm, izm] == 3)
                    {
                        x = 0;
                    }
                    if (DataBase.bok[neizm, izm] != 2)
                    {
                        ti.Wait();
                        mexanstr(ref neizm, ref izm, ref k1, ref k3, ref a1, ref b1, ref t, ref p, 2, ref neu,1);
                    }
                }
                else
                {
                    x = 0;
                }
            }
            if (i == 1)
            {
                izm -= pogr;
            }
            if (i == 2)
            {
                izm += pogr;
            }
            if (neu == 1)
            {
                g += 1;
            }
            else if (neu != 1)
            {
                g = 1;
            }
        }
        void obvod(ref int a,ref int b,int g, ref int t,ref int neu)
        {
            int p=0, j=0;
            for (int i = 0; i < 3; i++)
            {
                for (int o = 0; o < 3; o++)
                {
                    if (g == 1)
                    {
                        if ((a - 1 + i-2) < 10 && (b - 1 + o-2) < 10 && (a - 1 + i-2) >= 0 && (b - 1 + o-2) >=0)
                        {
                            dataGridView1.Rows[a - 1 + i-2].Cells[b - 1 + o-2].Style.BackColor = Color.Gray;
                        }                        
                        if (DataBase.bok[a - 1 + i, b - 1 + o] == 3)
                        {
                            p = a - 1 + i;
                            j = b - 1 + o;
                            t = 1;
                        }
                        DataBase.bok[a - 1 + i, b - 1 + o] = 2;
                    }
                    if (g == 2)
                    {
                        if ((a - 1 + i) < 10 && (b - 1 + o) < 10 && (a - 1 + i ) >= 0 && (b - 1 + o) >= 0)
                        {
                            dataGridView2.Rows[a - 1 + i].Cells[b - 1 + o].Style.BackColor = Color.Gray;
                        }                        
                        if (DataBase.bok2[a - 1 + i+2, b - 1 + o+2] == 3)
                        {
                            p = a - 1 + i;
                            j = b - 1 + o;
                            t = 1;
                        }
                        DataBase.bok2[a + 2 - 1 + i, b + 2 - 1 + o] = 2;
                    }                    
                }
            }
            if (t == 1)
            {
                a = p;
                b = j;
            }
            if (t != 1)
            {
                neu = 0;
            }
        }


        void provpop(int a, int b , int g,ref int k1, ref int k3,ref int a1,ref int b1)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int o = 0; o < 3; o++)
                {
                    if (g == 1)
                    {
                        if (DataBase.bok[a - 1 + i, b - 1 + o] == 1)
                        {
                            k1++;
                        }
                        if (DataBase.bok[a - 1 + i, b - 1 + o] == 3)
                        {
                            k3++;
                            a1 = a - 1 + i;
                            b1 = b - 1 + o;
                        }

                    }
                    if (g == 2)
                    {
                        if (DataBase.bok2[a - 1 + i, b - 1 + o] == 1)
                        {
                            k1++;
                        }
                        if (DataBase.bok2[a - 1 + i, b - 1 + o] == 3)
                        {
                            k3++;
                            a1 = a - 1 + i;
                            b1 = b - 1 + o;
                        }
                    }
                }
            }

        }
        void dopprov(int a,int b,int g, ref int k1)
        {
            int k3 = 0, a1 = 0, b1 = 0;
            provpop(a, b, g, ref k1, ref k3, ref a1, ref b1);
            if (k3 > 1)
            {
                DataBase.bok2[a1, b1] = 4;
                dopprov(a1, b1, g, ref k1);
            }
            DataBase.bok2[a1, b1] = 3;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    spavn(1 + j, 1 + i, 3 - j);
                }
            }
            label1.Text = "Вы ходите";
            label1.ForeColor = Color.Green;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int a, b, v1 = 0, v2 = 0;
            a = e.RowIndex;
            b = e.ColumnIndex;
            if (DataBase.bok2[a+2, b+2] != 2)
            {
                str(e,1);
            }
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (DataBase.bok[i,j] != 1)
                    {
                        v1++;
                    }
                    if (DataBase.bok2[ i,j] != 1)
                    {
                        v2++;
                    }
                }
            }
            if (v1 == 225)
            {
                this.Hide();
                if (MessageBox.Show("Вы проиграли") == DialogResult.OK)
                {
                    this.Close();
                }
            }
            if (v2 == 225)
            {
                this.Hide();
                if (MessageBox.Show("Вы выйграли") == DialogResult.OK)
                {
                    this.Close();
                }

            }
            label7.Text = $"{po2}";
            label7.ForeColor = Color.Green;
            label9.Text = $"{pr2}";
            label9.ForeColor = Color.Red;

        }
    }
}