using ChessBoardModel;

namespace ChessBoardGUI
{
    public partial class Form1 : Form
    {
        // ref to the class Board. Contains the values of board
        static Board myBoard = new Board(8);

        // 2D array of buttons whos values are determined by myBoard
        public Button[,] btnGrid = new Button[myBoard.Size, myBoard.Size];


        public Form1()
        {
            InitializeComponent();
            populateGrid();
        }

        private void populateGrid()
        {
            // create buttons and place them into panel1

            int buttonSize = panel1.Width / myBoard.Size;

            // make the panel a square
            panel1.Height = panel1.Width;

            // nested loop. creates buttons and prints them to the screen
            for (int i = 0; i < myBoard.Size; i++)
            {
                for (int j = 0; j < myBoard.Size; j++)
                {
                    btnGrid[i, j] = new Button();

                    btnGrid[i, j].Height = buttonSize;
                    btnGrid[i, j].Width = buttonSize;

                    //add click event for each button
                    btnGrid[i, j].Click += GridButtonClick;

                    // add the new button to the panel
                    panel1.Controls.Add(btnGrid[i, j]);

                    // set the location of the new button
                    btnGrid[i, j].Location = new Point(i * buttonSize, j * buttonSize);

                    btnGrid[i, j].Text = i + "|" + j;
                    btnGrid[i, j].Tag = new Point(i, j);
                }

            }
        }

        private void GridButtonClick(object? sender, EventArgs e)
        {
            //check if a piece has been selected
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Please select an item from the dropdown");
            }
            else
            {
                string m = comboBox1.SelectedItem as string;


                //Get the row and col number of the button clicked
                Button clickedButton = (Button)sender;
                Point location = (Point)clickedButton.Tag;

                int x = location.X;
                int y = location.Y;

                Cell currentCell = myBoard.theGrid[x, y];

                //determine legal next moves
                myBoard.MarkNextLegalMoves(currentCell, m);

                //update the text on each button
                for (int i = 0; i < myBoard.Size; i++)
                {
                    for (int j = 0; j < myBoard.Size; j++)
                    {
                        btnGrid[i, j].Text = "";

                        if (myBoard.theGrid[i, j].LegalNextMove == true)
                        {
                            btnGrid[i, j].Text = "Legal";
                            ForeColor = Color.Green;
                        }
                        else if (myBoard.theGrid[i, j].CurrentlyOccupied == true)
                        {
                            btnGrid[i, j].Text = m;
                        }
                    }
                }
            }
        }
    }
}