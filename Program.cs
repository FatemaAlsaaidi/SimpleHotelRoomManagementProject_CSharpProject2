using System.Collections.Generic;
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
        static double[] TotalCost= new double[MAX_ROOMS] ;
        static int RoomCounter = 0;
        static char ChoiceChar = 'y';
        static int roomNum;
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
            char ChoiceChar = 'y';
            bool AddMore = true;
            bool isUnique = false;
            //int roomNum = 0;
            //double dailyRate = 0;
            while (AddMore && RoomCounter < MAX_ROOMS)
            {
                Tries = 0;
                // Input unique room number
                while (Tries < 3)
                {
                    Console.WriteLine($"Enter the number of room {RoomCounter + 1}: ");
                    string input = Console.ReadLine();

                    if (!int.TryParse(input, out roomNum))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                        Tries++;
                        continue;
                    }

                    bool exists = false;
                    for (int i = 0; i < RoomCounter; i++)
                    {
                        if (roomNumbers[i] == roomNum)
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (exists)
                    {
                        Console.WriteLine("This room number already exists. Please enter a unique number.");
                        Tries++;
                    }
                    else
                    {
                        isUnique = true;
                        IsSave = true;
                        break;
                    }
                }

                if (!isUnique)
                {
                    Console.WriteLine("Failed to provide a unique room number after 3 tries.");
                    break;
                }

                // Input valid daily rate
                bool isValidRate = false;
                while (Tries < 3)
                {
                    Console.WriteLine($"Enter the daily rate of room {RoomCounter + 1}: ");
                    string input = Console.ReadLine();

                    if (!double.TryParse(input, out dailyRate))
                    {
                        Console.WriteLine("Invalid input. Please enter a numeric value.");
                        Tries++;
                        //continue;
                    }

                    if (dailyRate <= 100)
                    {
                        Console.WriteLine("Daily rate must be at least 100.");
                        Tries++;
                    }
                    else
                    {
                        isValidRate = true;
                        IsSave = true;
                        break;
                    }
                }

                if (!isValidRate)
                {
                    Console.WriteLine("Failed to provide a valid daily rate after 3 tries.");
                    break;
                }





                if (IsSave == false)
                {
                    Console.WriteLine("Room information did not save");
                    break;

                }
                else
                {
                    roomNumbers[RoomCounter] = roomNum;
                    DailyRates[RoomCounter] = dailyRate;
                    Console.WriteLine("Room information Add Successfully");
                    Tries = 0;
                    Console.WriteLine("Do you want add another room information ? y / n");
                    ChoiceChar = Console.ReadKey().KeyChar;
                    Console.ReadKey();
                    Console.WriteLine();
                    RoomCounter++;
                }
                if (RoomCounter >= MAX_ROOMS)
                {
                    Console.WriteLine("Cannot add more room. Maximum limit reached.");
                    AddMore = false;
                    break;
                }
                Console.WriteLine();
                if (ChoiceChar != 'y' && ChoiceChar != 'Y')
                {
                    AddMore = false;

                }
            }
        }
        //2. View All Rooms (Available + Reserved)
        static void ViewAllRooms()
        {
            if (RoomCounter == 0)
            {
                Console.WriteLine("No room available.");
                return;
            }
            bool hasAvailableRooms = false;
            Console.WriteLine("The Available Rooms Are:");

            for (int i = 0; i < RoomCounter; i++)
            {
                if (!isReserved[i])
                {
                    hasAvailableRooms = true;
                    Console.WriteLine($"Room {i + 1}:");
                    Console.WriteLine($"Room Number: {roomNumbers[i]}");
                    Console.WriteLine($"Daily Rate: {DailyRates[i]}");
                    Console.WriteLine("-------------------------");
                }
            }

            if (!hasAvailableRooms)
            {
                Console.WriteLine("There are no rooms available.");
            }

            bool hasReserveRoom = false;
            Console.WriteLine("The Reserve Room Are: ");
            for (int i =0; i< RoomCounter; i++)
            {
                if (isReserved[i])
                {
                    hasReserveRoom = true;
                    Console.WriteLine($"Room {i + 1}:");
                    Console.WriteLine($"Room Number: {roomNumbers[i]}");
                    Console.WriteLine($"Mark: {DailyRates[i]}");
                    Console.WriteLine($"Guest Name: {guestNames[i]}");
                    Console.WriteLine($"Total Cost: {TotalCost[i]}");
                    Console.WriteLine("-------------------------");
                }
            }
            if (!hasReserveRoom)
            {
                Console.WriteLine("There is no room reseve");
            }

        }
        //3. Reserve a Room
        static void ReserveRoom()
        {
            Console.Write("Enter the guest name: ");
            string guestName = Console.ReadLine();

            Console.Write("Enter the room number: ");
            int roomNumber;
            bool isRoomValid = int.TryParse(Console.ReadLine(), out roomNumber);
            if (!isRoomValid)
            {
                Console.WriteLine("Invalid room number.");
                return;
            }

            // Find room index in allRooms
            int roomIndex = Array.IndexOf(roomNumbers, roomNumber);

            // Validation: Room must exist
            if (roomIndex == -1)
            {
                Console.WriteLine("Room does not exist.");
                return;
            }

            // Validation: Room must not already be reserved
            if (isReserved[roomIndex])
            {
                Console.WriteLine("Room is already reserved.");
                return;
            }

            Console.Write("Enter number of nights: ");
            int numberOfNights;
            bool nightsValid = int.TryParse(Console.ReadLine(), out numberOfNights);
            if (!nightsValid || numberOfNights <= 0)
            {
                Console.WriteLine("Invalid number of nights. Must be greater than 0.");
                return;
            }

            // Reserve the room
            isReserved[roomIndex] = true;
            guestNames[roomIndex] = guestName;
            nights[roomIndex] = numberOfNights;
            TotalCost[roomIndex] = nights[roomIndex] * DailyRates[roomIndex];


            Console.WriteLine($"Room {roomNumber} reserved for {guestName} for {numberOfNights} nights. And the total cost is {TotalCost}");
        }

        //4. View All Reservations with total cost
        static void ViewAllReservations()
        {
            bool hasReserveRoom = false;
            Console.WriteLine("View All Reserviation: ");
            for (int i = 0; i < RoomCounter; i++)
            {
                if (isReserved[i] == true)
                {
                    hasReserveRoom = true;
                    Console.WriteLine($"Room {i + 1}:");
                    Console.WriteLine($"Room Number: {roomNumbers[i]}");
                    Console.WriteLine($"Daily Rate: {DailyRates[i]}");
                    Console.WriteLine($"Number of Nights: {nights[i]}");
                    Console.WriteLine($"Total Cost: {nights[i] * DailyRates[i] }");
                    Console.WriteLine("-------------------------");
                }
            }

            if (!hasReserveRoom)
            {
                Console.WriteLine("There is no room reseve");
            }
        }
        //5. Search Reservation by Guest Name
        static void SearchReservationByGuestName()
        {
            bool FoundGuestName = false;
            Console.WriteLine("Enter the guest name: ");
            string UserGuestName = Console.ReadLine() ;

            for (int i=0; i<RoomCounter; i++)
            {
                if (guestNames[i] == UserGuestName)
                {
                    FoundGuestName = true;
                    Console.WriteLine($"Room {i + 1}:");
                    Console.WriteLine($"Room Number: {roomNumbers[i]}");
                    Console.WriteLine($"Daily Rate: {DailyRates[i]}");
                    Console.WriteLine($"Number of Nights: {nights[i]}");
                    Console.WriteLine($"Total Cost: {nights[i] * DailyRates[i]}");
                    Console.WriteLine("-------------------------");

                }
            }

            if (!FoundGuestName)
            {
                Console.WriteLine("The Guest Name do not found");
            }
        }
        //6. Highest-Paying Guest 
        static void HighestPayingGuest()
        {
            double HghitPayingGuest = 0;
            int index = 0;
            for (int i=0; i< RoomCounter; i++)
            {
                if (TotalCost[i] > HghitPayingGuest)
                {
                    HghitPayingGuest = TotalCost[i];
                    index = i;
                }
            }

            Console.WriteLine("The Highest Paying Guest is:");
            Console.WriteLine($"Guest Name: {guestNames[index]}");
            Console.WriteLine($"Room Number: {roomNumbers[index]}");
            Console.WriteLine($"Daily Rate: {DailyRates[index]}");
            Console.WriteLine($"Number of Nights: {nights[index]}");
            Console.WriteLine($"Total Cost: {TotalCost[index]}");
            Console.WriteLine("-------------------------");


        }
        //7. Cancel Reservation by Room Number
        static void CancelReservationByRoomNumber()
        {
            Console.Write("Enter the room number to cancel reservation: ");
            string input = Console.ReadLine();
            int cancelNum;

            if (!int.TryParse(input, out cancelNum))
            {
                Console.WriteLine("Invalid room number format.");
                return;
            }

            int index = -1;

            // Search for the room
            for (int i = 0; i < RoomCounter; i++)
            {
                if (roomNumbers[i] == cancelNum)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                Console.WriteLine("Room number not found.");
                return;
            }

            // Check if the room is reserved
            if (!isReserved[index])
            {
                Console.WriteLine("This room is not currently reserved.");
                return;
            }

            // Confirm cancellation
            Console.Write($"Are you sure you want to cancel the reservation for room {cancelNum}? (y/n): ");
            char confirm = Console.ReadKey().KeyChar;
            Console.WriteLine();

            if (char.ToLower(confirm) == 'y')
            {
                isReserved[index] = false;
                guestNames[index] = "";
                nights[index] = 0;

                Console.WriteLine($"Reservation for room {cancelNum} has been successfully cancelled.");
            }
            else
            {
                Console.WriteLine("Cancellation aborted.");
            }
        }



    }
}
