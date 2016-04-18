using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsProgrammingLab3
{
    public partial class Form1 : Form
    {
        private Color shapeColor = Color.Black;
        private int[] clicks = { 2, 3, 4, 6 };
        private int pointIndex = -1;
        private int mode;//1 for segment, 2 for tri..etc
        private bool newButtonClicked = false;
        private bool shapeSelected = false;
        private bool isSelected = false;
        private PointF currentPoint;//the point we are dragging
        List<PointF> currentPoints;
        List<PointF> currentShape;
        List<List<PointF>> allPoints;
        List<Color> colors;
        private float stepSize;
        Color currentColor;//refers to the color of the shape to be translated
        private Button[] buttons = new Button[4];
        public Form1()
        {
            InitializeComponent();
            panel1.BackColor = Color.White;
            panel2.BackColor = shapeColor;
            mode = 0;
            buttons[0] = SegmentButton;
            buttons[1] = TriangleButton;
            buttons[2] = QuadrangleButton;
            buttons[3] = HexagonButton;
            currentPoints = new List<PointF>();
            currentShape = new List<PointF>();
            allPoints = new List<List<PointF>>();
            colors = new List<Color>();
            currentColor = shapeColor;
            stepSize = 5.0f;
            this.AcceptButton = null;
            this.DoubleBuffered = true;

            currentPoint = new PointF();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                // Set form background to the selected color.

                panel2.BackColor = colorDialog1.Color;
                shapeColor = colorDialog1.Color;
            }
        }

        private void SegmentButton_Click(object sender, EventArgs e)
        {
            mode = 1;
            label1.Text = "Click " + 2 + " times";

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Enabled = false;
            }

        }

        private void TriangleButton_Click(object sender, EventArgs e)
        {
            mode = 2;
            label2.Text = "Click " + 3 + " times";
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Enabled = false;
            }



        }

        private void QuadrangleButton_Click(object sender, EventArgs e)
        {
            mode = 3;
            label3.Text = "Click " + 4 + " times";
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Enabled = false;
            }



        }

        private void HexagonButton_Click(object sender, EventArgs e)
        {
            mode = 4;

            label4.Text = "Click " + 6 + " times";
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Enabled = false;
            }


        }

        private void panel1_Click(object sender, EventArgs e)

        {
            Point point = panel1.PointToClient(Cursor.Position);
            PointF pointf = new PointF(point.X, point.Y);

            switch (mode)
            {

                case 1:


                    if (clicks[0]-- > 0)
                    {
                        if (clicks[0] <= 0)
                            panel1.Invalidate();
                        label1.Text = "Click " + clicks[0] + " times";
                        if (point != null)
                            currentPoints.Add(point);
                    }

                    break;
                case 2:

                    if (clicks[1]-- > 0)
                    {
                        if (clicks[1] <= 0)
                            panel1.Invalidate();

                        label2.Text = "Click " + clicks[1] + " times";
                        currentPoints.Add(point);
                    }

                    break;
                case 3:

                    if (clicks[2]-- > 0)
                    {
                        if (clicks[2] <= 0)
                            panel1.Invalidate();
                        label3.Text = "Click " + clicks[2] + " times";
                        currentPoints.Add(point);
                    }

                    break;
                case 4:

                    if (clicks[3]-- > 0)
                    {
                        if (clicks[3] <= 0)
                            panel1.Invalidate();
                        label4.Text = "Click " + clicks[3] + " times";
                        currentPoints.Add(point);
                    }

                    break;

            }
            if (currentShape.Count > 0 && shapeSelected)
            {

                shapeSelected = false;
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].Enabled = true;
                }
                panel1.Invalidate();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (pointIndex >= 0 && isSelected)
                allPoints[allPoints.Count - 1][pointIndex] = new PointF(currentPoint.X, currentPoint.Y);

            Graphics g = e.Graphics;
            List<PointF> tempPoints = new List<PointF>(currentPoints);


            switch (mode)
            {
                case 1:

                    if (clicks[0] <= 0)
                    {
                        colors.Add(shapeColor);
                        allPoints.Add(tempPoints);
                        currentPoints.Clear();
                        currentShape = tempPoints;
                        currentColor = shapeColor;
                        mode = 0;
                        clicks[0] = 2;
                        for (int i = 0; i < buttons.Length; i++)
                        {
                            buttons[i].Enabled = true;
                        }

                    }
                    break;
                case 2:
                    if (clicks[1] <= 0)
                    {
                        colors.Add(shapeColor);
                        allPoints.Add(tempPoints);
                        currentPoints.Clear();
                        currentShape = tempPoints;
                        currentColor = shapeColor;
                        mode = 0;
                        clicks[1] = 3;
                        for (int i = 0; i < buttons.Length; i++)
                        {
                            buttons[i].Enabled = true;
                        }
                    }
                    break;
                case 3:
                    if (clicks[2] <= 0)
                    {
                        colors.Add(shapeColor);
                        allPoints.Add(tempPoints);
                        currentPoints.Clear();
                        currentShape = tempPoints;
                        currentColor = shapeColor;
                        mode = 0;
                        clicks[2] = 4;
                        for (int i = 0; i < buttons.Length; i++)
                        {
                            buttons[i].Enabled = true;
                        }
                    }
                    break;
                case 4:
                    if (clicks[3] <= 0)
                    {
                        colors.Add(shapeColor);
                        allPoints.Add(tempPoints);
                        currentPoints.Clear();
                        currentShape = tempPoints;
                        currentColor = shapeColor;//currentShape is always drawn on top, sometimes it is the most recently edited shape sometimes it is not
                        mode = 0;
                        clicks[3] = 6;
                        for (int i = 0; i < buttons.Length; i++)
                        {
                            buttons[i].Enabled = true;
                        }
                    }
                    break;


            }
            if (!newButtonClicked)
            {
                for (int i = 0; i < allPoints.Count; i++)
                {
                    if (allPoints[i].Count > 2)
                    {

                        g.FillPolygon(new SolidBrush(colors[i]), allPoints[i].ToArray());
                    }

                    else
                        g.DrawLines(new Pen(colors[i], 3), allPoints[i].ToArray());
                }

            }
            else//delete all shapes since new button was clicked
            {
                for (int i = 0; i < allPoints.Count; i++)
                {
                    allPoints[i].Clear();

                }
                allPoints.Clear();
                colors.Clear();
                newButtonClicked = false;

            }
            if (allPoints.Count > 0)
            {

                if (allPoints[allPoints.Count - 1].Count > 2 && shapeSelected)
                {
                    g.FillPolygon(new SolidBrush(colors[colors.Count - 1]), allPoints[allPoints.Count - 1].ToArray());
                    g.DrawPolygon(new Pen(Color.Yellow, 3), allPoints[allPoints.Count - 1].ToArray());
                }

                else if (allPoints[allPoints.Count - 1].Count == 2 && shapeSelected)
                {
                    g.DrawPolygon(new Pen(Color.Yellow, 5), allPoints[allPoints.Count - 1].ToArray());
                    g.DrawLines(new Pen(colors[colors.Count - 1], 3), allPoints[allPoints.Count - 1].ToArray());

                }
            }


        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Invalidate();
            newButtonClicked = true;
            shapeSelected = false;
        }

        private void panel1_DoubleClick(object sender, EventArgs e)//select shape and enter dragging mode
        {
            //TODO if we don't want to move currentShape to the top we must creat a diff global other than currentSHape that is not repainted
            Point tempPoint = panel1.PointToClient(Cursor.Position);
            PointF clickedPoint = new PointF(tempPoint.X, tempPoint.Y);

            for (int i = 0; i < allPoints.Count; i++)
            {
                if (allPoints[i].Count > 2)
                {
                    if (IsPointInPolygon(allPoints[i].ToArray(), clickedPoint))//i is the index
                    {
                        currentShape = allPoints[i];//set some global variable to be the current shape, handle its movement in KeyDown events
                        currentColor = colors[i];
                        allPoints.RemoveAt(i);
                        colors.RemoveAt(i);
                        allPoints.Add(currentShape);
                        colors.Add(currentColor);
                        shapeSelected = true;
                        for (int j = 0; j < buttons.Length; j++)
                        {
                            buttons[j].Enabled = false;
                        }
                        panel1.Invalidate();



                    }
                }
                else if (allPoints[i].Count == 2)
                {
                    if (IsOnLine(allPoints[i][0], allPoints[i][1], clickedPoint, 5))//5 pixels is the allowed tolerance in pixels of how close to the line we clicked
                    {
                        currentShape = allPoints[i];//set some global variable to be the current shape, handle its movement in KeyDown events
                        currentColor = colors[i];
                        allPoints.RemoveAt(i);
                        colors.RemoveAt(i);
                        allPoints.Add(currentShape);
                        colors.Add(currentColor);
                        shapeSelected = true;
                        for (int j = 0; j < buttons.Length; j++)
                        {
                            buttons[j].Enabled = false;
                        }

                        panel1.Invalidate();



                    } //we need a special case to handle line collision detection using GraphicsPathObject
                }
            }
        }

        public bool IsPointInPolygon(PointF[] polygon, PointF testPoint)//polygon collision detection (GraphicsPath built in collision just like in IsOnLine method is also possible here)
        {
            bool result = false;
            int j = polygon.Count() - 1;
            for (int i = 0; i < polygon.Count(); i++)
            {
                if (polygon[i].Y < testPoint.Y && polygon[j].Y >= testPoint.Y || polygon[j].Y < testPoint.Y && polygon[i].Y >= testPoint.Y)
                {
                    if (polygon[i].X + (testPoint.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X) < testPoint.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }
        bool IsOnLine(PointF p1, PointF p2, PointF p, int width = 1)//line collision detection
        {
            var isOnLine = false;
            using (var path = new GraphicsPath())
            {
                using (var pen = new Pen(Brushes.Black, width))
                {
                    path.AddLine(p1, p2);
                    isOnLine = path.IsOutlineVisible(p, pen);
                }
            }
            return isOnLine;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)

        {
            this.Focus();
            //check what key is pressed and translate all points in currentShape by a global stepSize
            if (currentShape.Count > 0 && shapeSelected)//means the shape exists
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:

                        for (int i = 0; i < currentShape.Count; i++)
                            currentShape[i] = new PointF(currentShape[i].X, currentShape[i].Y - stepSize);//this is necessary because one cannot modify the exact field here. See http://stackoverflow.com/questions/1747654/cannot-modify-the-return-value-error-c-sharp
                        panel1.Invalidate();
                        break;
                    case Keys.Left:
                        for (int i = 0; i < currentShape.Count; i++)
                            currentShape[i] = new PointF(currentShape[i].X - stepSize, currentShape[i].Y);
                        panel1.Invalidate();
                        break;
                    case Keys.Right:
                        for (int i = 0; i < currentShape.Count; i++)
                            currentShape[i] = new PointF(currentShape[i].X + stepSize, currentShape[i].Y);
                        panel1.Invalidate();
                        break;
                    case Keys.Down:
                        for (int i = 0; i < currentShape.Count; i++)
                            currentShape[i] = new PointF(currentShape[i].X, currentShape[i].Y + stepSize);
                        panel1.Invalidate();
                        break;







                }
            }
        }
        private void SegmentButton_Enter(object sender, EventArgs e)
        {
            this.Focus();
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)//paint everything and save to bitmap
        {

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK
           && saveFileDialog1.FileName.Length > 0)
            {
                Bitmap bmap = new Bitmap(panel1.Width, panel1.Height);
                using (Graphics g = Graphics.FromImage(bmap))
                {

                    if (!newButtonClicked)
                    {
                        for (int i = 0; i < allPoints.Count; i++)
                        {
                            if (allPoints[i].Count > 2)
                                g.FillPolygon(new SolidBrush(colors[i]), allPoints[i].ToArray());
                            else
                                g.DrawLines(new Pen(colors[i], 3), allPoints[i].ToArray());
                        }

                    }
                    else//delete all shapes since new button was clicked
                    {
                        for (int i = 0; i < allPoints.Count; i++)
                        {
                            allPoints[i].Clear();

                        }
                        allPoints.Clear();
                        colors.Clear();
                        newButtonClicked = false;

                    }

                    if (allPoints.Count > 0)
                    {

                        if (allPoints[allPoints.Count - 1].Count > 2 && shapeSelected)
                        {
                            g.FillPolygon(new SolidBrush(colors[colors.Count - 1]), allPoints[allPoints.Count - 1].ToArray());
                            g.DrawPolygon(new Pen(Color.Yellow, 3), allPoints[allPoints.Count - 1].ToArray());
                        }

                        else if (allPoints[allPoints.Count - 1].Count == 2 && shapeSelected)
                        {
                            g.DrawPolygon(new Pen(Color.Yellow, 5), allPoints[allPoints.Count - 1].ToArray());
                            g.DrawLines(new Pen(colors[colors.Count - 1], 3), allPoints[allPoints.Count - 1].ToArray());

                        }
                    }


                    bmap.Save(saveFileDialog1.FileName + ".bmp");

                }
            }


        }


        //drag vertices
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Point tempPoint = panel1.PointToClient(Cursor.Position);
            PointF clickedPoint = new PointF(tempPoint.X, tempPoint.Y);
            //tolerance of 3 pixels
            for (int i = 0; i < allPoints.Count; i++)
            {
                for (int j = 0; j < allPoints[i].Count; j++)
                {
                    if (clickedPoint.X <= allPoints[i][j].X + 3 && clickedPoint.X >= allPoints[i][j].X - 3 && clickedPoint.Y <= allPoints[i][j].Y + 3 && clickedPoint.Y >= allPoints[i][j].Y - 3)
                    {
                        pointIndex = j;
                        currentShape = allPoints[i];//set some global variable to be the current shape, handle its movement in KeyDown events
                        currentColor = colors[i];
                        allPoints.RemoveAt(i);
                        colors.RemoveAt(i);
                        allPoints.Add(currentShape);
                        colors.Add(currentColor);
                        isSelected = true;
                    }
                }
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelected)
            {
                Point tempPoint = panel1.PointToClient(Cursor.Position);
                currentPoint.X = tempPoint.X;
                currentPoint.Y = tempPoint.Y;
                panel1.Invalidate();
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isSelected = false;
            panel1.Invalidate();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].Enabled = true;
                }
                shapeSelected = false;
                panel1.Invalidate();// Enter key pressed
            }

        }

    }
}

