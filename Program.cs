namespace SimpleHotelRoomManagementProject_CSharpProject2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            int choiceNum;
            //While loop to repeat the process, and set true to not stop the loop
            while (true)
            {

                try
                {
                    // Just to clear the screen
                    Console.Clear();
                    //Print the menu lists
                    Console.WriteLine("Simple Students Management");
                    Console.WriteLine("1. Add a new room");
                    Console.WriteLine("2. View all rooms");
                    Console.WriteLine("3. Reserve a room for a guest");
                    Console.WriteLine("4. View all reservations with total cost");
                    Console.WriteLine("5. Search reservation by guest name");
                    Console.WriteLine("6. Find the highest-paying guest");
                    Console.WriteLine("7. Cancel a reservation by room number ");
                    Console.WriteLine("0. Exit ");
                    Console.Write("Enter The Number Of Feature: ");
                    // store the enter number in the variable
                    choiceNum = int.Parse(Console.ReadLine());

                    // Tasks Functons
                    switch (choiceNum)
                    {
                        case 1: AddNewRoom(); break;
                        case 2: AddNewRoom(); break;
                        case 3: AddNewRoom(); break;
                        case 4: AddNewRoom(); break;
                        case 5: AddNewRoom(); break;
                        case 6: AddNewRoom(); break;
                        case 7: AddNewRoom(); break;
                        case 0: return;
                        default: Console.WriteLine("Invalid choice! Try again."); break;
                    }
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    // Display error message to user
                    Console.WriteLine($"Error: {e.Message}");
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadKey();  // Wait for user input before clearing the screen

                }
            }
        }
        static void AddNewRoom()
        {

        }
    }
}
