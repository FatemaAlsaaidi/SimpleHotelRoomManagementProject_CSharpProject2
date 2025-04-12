using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
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
            //declare nedded variables
            char ChoiceChar = 'y';
            bool AddMore = true;
            bool isUnique = false;
            // use while to repate code until fail the condition
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
                    // check if input data is exist or no.
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
                // check if the number room is unique or no 
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
                    // validate a numeric value of rating input data 
                    if (!double.TryParse(input, out dailyRate))
                    {
                        Console.WriteLine("Invalid input. Please enter a numeric value.");
                        Tries++;
                        //continue;
                    }
                    // check if daily rate is greater than 100 or no 
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




                // use if statement to save inputs data if they are value and able to save.
                if (IsSave == false)
                {
                    Console.WriteLine("Room information did not save");
                    break;

                }
                else
                {
                    // if all inputs data is valiable save them in the array 
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
                // if statement to check Maximum limit reach
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
                else
                {
                    AddMore = true;
                }
            }
        }
        //2. View All Rooms (Available + Reserved)
        static void ViewAllRooms()
        {
            // Check if there are no rooms in the system
            if (RoomCounter == 0)
            {
                // Display a message indicating that there are no rooms available.
                Console.WriteLine("No room available.");
                // Exit the method if there are no rooms.
                return; 
            }
            // Initialize a boolean variable to track if there are any available rooms.
            bool hasAvailableRooms = false;
            Console.WriteLine("The Available Rooms Are:");
            // Iterate through all the rooms in the system.
            for (int i = 0; i < RoomCounter; i++)
            {
                // Check if the current room is not reserved.
                if (!isReserved[i])
                {
                    // Set the flag to true, indicating that at least one available room is found.
                    hasAvailableRooms = true;
                    // Display the details of the available room.
                    Console.WriteLine($"Room {i + 1}:");
                    Console.WriteLine($"Room Number: {roomNumbers[i]}");
                    Console.WriteLine($"Daily Rate: {DailyRates[i]}");
                    Console.WriteLine("-------------------------");
                }
            }
            // After the loop, check if no available rooms were found.
            if (!hasAvailableRooms)
            {
                Console.WriteLine("There are no rooms available.");
            }
            // Initialize a boolean variable to track if there are any reserved rooms.
            bool hasReserveRoom = false;
            Console.WriteLine("The Reserve Room Are: ");
            // Iterate through all the rooms in the system again.
            for (int i =0; i< RoomCounter; i++)
            {
                // Check if the current room is reserved.
                if (isReserved[i])
                {
                    // Set the flag to true, indicating that the reserved room is found.
                    hasReserveRoom = true;
                    // Display the details of the reserved room.
                    Console.WriteLine($"Room {i + 1}:");
                    Console.WriteLine($"Room Number: {roomNumbers[i]}");
                    Console.WriteLine($"Mark: {DailyRates[i]}");
                    Console.WriteLine($"Guest Name: {guestNames[i]}");
                    Console.WriteLine($"Total Cost: {TotalCost[i]}");
                    Console.WriteLine("-------------------------");
                }
            }
            // After the loop, check if no reserved rooms were found.
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
            // Initialize a boolean variable to track if room has reserve or no, setting it to false initially
            bool hasReserveRoom = false;
            Console.WriteLine("View All Reserviation: ");

            // Start a for loop that iterates through the reservations, to print room which reserve
            for (int i = 0; i < RoomCounter; i++)
            {
                // check if the value of IsReserved room for current index is true
                if (isReserved[i] == true)
                {
                    // If the the room is reserv, set the hasReserveRoom flag to true.
                    hasReserveRoom = true;
                    Console.WriteLine($"Room {i + 1}:");
                    Console.WriteLine($"Room Number: {roomNumbers[i]}");
                    Console.WriteLine($"Daily Rate: {DailyRates[i]}");
                    Console.WriteLine($"Number of Nights: {nights[i]}");
                    Console.WriteLine($"Total Cost: {nights[i] * DailyRates[i] }");
                    Console.WriteLine("-------------------------");
                }
            }
            // use if statment to check hasReserveRoom if it is still values means that There is no room reseve untill now.
            if (!hasReserveRoom)
            {
                Console.WriteLine("There is no room reseve");
            }
        }
        //5. Search Reservation by Guest Name
        static void SearchReservationByGuestName()
        {
            // Initialize a boolean variable to track if a guest name is found, setting it to false initially.
            bool FoundGuestName = false;
            //ask user to enter the name of guest
            Console.WriteLine("Enter the guest name: ");
            // Read the guest name entered by the user from the console and store it in the UserGuestName string variable.
            string UserGuestName = Console.ReadLine() ;

            // Input validation: Check if the guest name is null or empty.
            if (string.IsNullOrEmpty(UserGuestName))
            {
                Console.WriteLine("Error: Guest name cannot be empty. Please enter a valid name.");
                return; // Exit the method if the guest name is invalid.
            }

            // Start a for loop that iterates through the reservations, using the RoomCounter variable to determine the number of iterations.
            for (int i=0; i<RoomCounter; i++)
            {
                // Check if the guest name at the current index (i) in the guestNames array is equal to the guest name entered by the user (UserGuestName).
                if (guestNames[i] == UserGuestName)
                {
                    // If the guest name is found, set the FoundGuestName flag to true.
                    FoundGuestName = true;
                    // Display the guest remaining information
                    Console.WriteLine($"Room {i + 1}:");
                    Console.WriteLine($"Room Number: {roomNumbers[i]}");
                    Console.WriteLine($"Daily Rate: {DailyRates[i]}");
                    Console.WriteLine($"Number of Nights: {nights[i]}");
                    Console.WriteLine($"Total Cost: {nights[i] * DailyRates[i]}");
                    Console.WriteLine("-------------------------");

                }
            }
            // After the loop finishes, check if the FoundGuestName flag is still false, indicating that no matching guest name was found.
            if (!FoundGuestName)
            {
                Console.WriteLine("The Guest Name do not found");
            }
        }
        //6. Highest-Paying Guest 
        static void HighestPayingGuest()
        {
            // Initialize variable to store the highest total cost found
            double HghitPayingGuest = 0;
            // Variable to keep track of the index of the highest paying guest
            int index = 0;
            // Loop through all rooms to find the guest with the highest total cost
            for (int i=0; i< RoomCounter; i++)
            {
                // Check if the current guest's total cost is higher than the highest found so far
                if (TotalCost[i] > HghitPayingGuest)
                {
                    HghitPayingGuest = TotalCost[i]; // Update the highest total cost
                    index = i; // Save the index of this guest
                }
            }
            // Display information about the highest paying guest
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
            // Ask the user to enter the room number they want to cancel
            Console.Write("Enter the room number to cancel reservation: ");
            // Read the input from the user
            string input = Console.ReadLine();
            // Declare a variable to store the validated room number
            int cancelNum;

            // Try to convert the input into a valid integer
            if (!int.TryParse(input, out cancelNum))
            {
                Console.WriteLine("Invalid room number format.");
                return;
            }
            // Initialize a variable to track the index of the room in the array
            int index = -1;

            // Search for the entered room number in the list of added rooms
            for (int i = 0; i < RoomCounter; i++)
            {
                if (roomNumbers[i] == cancelNum)
                {
                    index = i; // Room found, store its index
                    break; // Exit the loop early
                }
            }
            // Check if the room is currently reserved
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
                // Reset the reservation data for this room
                isReserved[index] = false;
                guestNames[index] = "";
                nights[index] = 0;
                // Notify the user of successful cancellation
                Console.WriteLine($"Reservation for room {cancelNum} has been successfully cancelled.");
            }
            else
            {
                // If the user chooses not to cancel, show cancellation aborted
                Console.WriteLine("Cancellation aborted.");
            }
        }



    }
}
