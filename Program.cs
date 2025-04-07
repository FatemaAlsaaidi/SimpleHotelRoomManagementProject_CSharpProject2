using System.Net.NetworkInformation;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimpleHotelRoomManagementProject_CSharpProject2
{
    internal class Program
    {
        static int MAX_ROOMS = 4; // the number of rooms vailable
        static int[] roomNumbers = new int[MAX_ROOMS]; //Room Numbers 
        static double[] DailyRates = new double[MAX_ROOMS]; //Daily Rate for each room 
        static bool[] isReserved = new bool[MAX_ROOMS]; //Reservation status for each room 
        static string[] guestNames = new string[MAX_ROOMS]; //Guest names(aligned with room numbers)
        static int[] nights = new int[MAX_ROOMS]; //Nights booked per reservation 
        static DateTime[] bookingDates = new DateTime[MAX_ROOMS]; //Set using DateTime.Now
        static int RoomCounter = 0;
        static char ChoiceChar = 'y';
        static int RoomNum;
        static double dailyRate;
        static bool IsSave = true;
        static int Tries = 0;
        //static bool AddMore = true;

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
                        case 2: ViewAllRooms(); break;
                        case 3: ReserveRoom(); break;
                        case 4: ViewAllReservations(); break;
                        case 5: SearchReservationByGuestName(); break;
                        case 6: HighestPayingGuest(); break;
                        case 7: CancelReservationByRoomNumber(); break;
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
        //1. Add a New Room 
        static void AddNewRoom()
        {
            bool AddMore = true;
            while (AddMore && RoomCounter < MAX_ROOMS)
            {
                //User enter uniqu room number
                do
                {
                    Console.WriteLine($"Enter the number of room {RoomCounter + 1}:");
                    RoomNum = int.Parse(Console.ReadLine());

                    for (int i = 0; i < MAX_ROOMS; i++)
                    {
                        if (RoomNum == roomNumbers[i])
                        {
                            Console.WriteLine("The number already exist, enter unique number please!");
                            IsSave = false;
                            break;
                        }
                    }

                    Tries++;
                    if (Tries >= 3)
                    {
                        Console.WriteLine("You exceeded tries limited");
                        IsSave = false;
                        break;
                    }
                } while (!IsSave);

                if (Tries >= 3)
                {
                    Console.WriteLine("You exceeded tries limited");
                    IsSave = false;
                }

                //User enter daily rate room
                do
                {
                    Console.WriteLine($"Enter the daily rate of room {RoomCounter + 1} ");
                    dailyRate = double.Parse(Console.ReadLine());

                    Tries++;
                    if (Tries >= 3)
                    {
                        Console.WriteLine("You exceeded tries limited");
                        IsSave = false;
                        break;
                    }

                } while (!IsSave);

                if (Tries >= 3)
                {
                    Console.WriteLine("You exceeded tries limited");
                    IsSave = false;
                }

                if (IsSave == false)
                {
                    Console.WriteLine("Room information did not save");
                    break;

                }
                else
                {
                    roomNumbers[RoomCounter] = RoomNum;
                    DailyRates[RoomCounter] = dailyRate;
                    Console.WriteLine("Room information Add Successfully");
                    Tries = 0;
                    Console.WriteLine("Do you want add another room information ? y / n");
                    ChoiceChar = Console.ReadKey().KeyChar;
                    Console.ReadKey();
                    Console.WriteLine();
                    RoomCounter++;
                }
                if (RoomCounter > MAX_ROOMS)
                {
                    Console.WriteLine("Cannot add more room. Maximum limit reached.");
                    break;
                }
            }
        }
        //2. View All Rooms (Available + Reserved)
        static void ViewAllRooms()
        {
            if (RoomCounter == 0)
            {
                Console.WriteLine("No students available.");
                return;
            }
            for (int i = 0; i < RoomCounter; i++)
            {
                Console.WriteLine("The Available Room Are: ");
                if (isReserved[i] == true)
                {
                    Console.WriteLine($"Room {i + 1}:");
                    Console.WriteLine($"Name: {roomNumbers[i]}");
                    Console.WriteLine($"Mark: {DailyRates[i]}");
                }

            }

            for (int i =0; i< RoomCounter; i++)
            {
                Console.WriteLine("The Reserve Room Are: ");
                if (isReserved[i] == false)
                {
                    Console.WriteLine($"Room {i + 1}:");
                    Console.WriteLine($"Name: {roomNumbers[i]}");
                    Console.WriteLine($"Mark: {DailyRates[i]}");
                }
            }

        }
        //3. Reserve a Room
        static void ReserveRoom()
        {
            bool AvailableRoom = true;
            Console.WriteLine("Enter the guest name : ");
            string GuestName = Console.ReadLine();

            Console.WriteLine("Enter the room number: ");
            int ReserveRoom = int.Parse(Console.ReadLine());

            for (int i =0; i< RoomCounter; i++)
            {
                if(ReserveRoom == roomNumbers[i])
                {
                    AvailableRoom = false;
                    Console.WriteLine("Room must not already be reserved ");
                }
            }

            if (AvailableRoom = true)
            {
                Console.WriteLine("Enter the number of night: ");
                int NightNum = int.Parse(Console.ReadLine());
            }



        }
        //4. View All Reservations 
        static void ViewAllReservations()
        {

        }
        //5. Search Reservation by Guest Name
        static void SearchReservationByGuestName()
        {
        }
        //6. Highest-Paying Guest 
        static void HighestPayingGuest()
        {

        }
        //7. Cancel Reservation by Room Number
        static void CancelReservationByRoomNumber()
        {

        }

    }
}
