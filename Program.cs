using CalculadoraDeEdad;


Console.WriteLine("---- Age Calculator ----");
bool loop = true;
int choice;
List<UserAge> users = new List<UserAge>();
UserAge? userAgeMenu = null;

while (loop)
{
    menu();
    if (int.TryParse(Console.ReadLine(), out choice))
    {
       
        switch (choice)
        {
            case 0: loop = false; break;
            case 1: userAgeMenu = calcularEdad(); break;
            case 2: if(userAgeMenu!=null) displayAge(userAgeMenu, false); break;
            case 3: if(userAgeMenu!=null) displayAge(userAgeMenu, true); break;
            case 4: datePlanetOutput(); break;
            case 5: calcularEdadFutura(); break;
            default: Console.WriteLine("\nNon-valid Option\n\n"); break;
        }

        if (userAgeMenu != null)
        {
            users.Add(userAgeMenu);
            userAgeMenu = null;
        }
            
    }
    else
        Console.WriteLine("Non-valid Choice\n\n");
}


void menu()
{
    Console.WriteLine("\nOptions: ");
    Console.WriteLine("1) Calculate User's Age");
    Console.WriteLine("2) Show Age in Different Planets");
    Console.WriteLine("3) Calculate User's Future Age\n");
    Console.WriteLine("0) Exit\n");
    Console.Write("Choice: ");
}


void subMenu(bool daysAndMonths)
{
    Console.WriteLine("\nWould you like to see your age in: ");
    Console.WriteLine("1) Years");
    if (daysAndMonths)
    {
        Console.WriteLine("2) Years or Months");
        Console.WriteLine("3) Years or Days");
        Console.WriteLine("4) Years, Months or Days\n");

    }
    else
    {
        Console.WriteLine("2) Years and Months");
        Console.WriteLine("3) Years and Days");
        Console.WriteLine("4) Years, Months and Days\n");
    }
    Console.WriteLine("0) Exit\n");
    Console.Write("Choice: ");
}


void subMenuPlanets()
{
    Console.WriteLine("\nWould you like to see your age in: ");
    Console.WriteLine("0) Mercury");
    Console.WriteLine("1) Venus");
    Console.WriteLine("2) Earth");
    Console.WriteLine("3) Mars");
    Console.WriteLine("4) Jupiter");
    Console.WriteLine("5) Saturn");
    Console.WriteLine("6) Uranus");
    Console.WriteLine("7) Neptune");
    Console.WriteLine("8) Pluto\n");
    Console.WriteLine("9) Exit\n");
    Console.Write("Choice: ");
}


string writeBirthDate()
{
    Console.Write("\nWrite your Date of Birth (dd/mm/aaaa): ");
    return Console.ReadLine();
}

string writeFutureDate()
{
    Console.Write("\nWrite Future Date (dd/mm/aaaa): ");
    return Console.ReadLine();
}

void displayAge(UserAge userAge, bool daysAndMonths)
{
    if (daysAndMonths)
        dateOutputFormatDaysAndMonths(userAge);
    else
        dateOutputFormat(userAge);
}

UserAge calcularEdad()
{
    bool repeat = true;
    string userInput;
    UserAge? userAge = null;

    while (repeat)
    {
        userInput = writeBirthDate();

        if (checkString(userInput) && DateTime.TryParseExact(userInput, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime birthDate))
        {

            DateTime today = DateTime.Today;

            userAge = calcAge(today, birthDate);
            userAge = calcAgeInDaysAndMonths(userAge, today, birthDate);
            userAge = calcAgeInPlanets(userAge);
            repeat = false;
        }
        else
        {
            Console.WriteLine("Format is Not Correct");
        }

    }
    return userAge;
}

void calcularEdadFutura()
{
    bool repeat = true;
    string userInputBirthDate, userInputFutureDate;

    while (repeat)
    {
        userInputBirthDate = writeBirthDate();

        if (checkString(userInputBirthDate) && DateTime.TryParseExact(userInputBirthDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime birthDate))
        {
            userInputFutureDate = writeFutureDate();

            if (checkString(userInputFutureDate) && DateTime.TryParseExact(userInputFutureDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime futureDate))
            {

                int comp = DateTime.Compare(birthDate, futureDate);

                if (comp < 0)
                {
                    UserAge userAge = calcAge(futureDate, birthDate);
                    Console.WriteLine("\nUser will have {0} years, {1} months, and {2} days\n\n", userAge.ageYears, userAge.ageMonths, userAge.ageDays);
                    repeat = false;
                }
                else if (comp == 0)
                {
                    Console.WriteLine("\n You Wrote the Same Date!!\n\n");
                }
                else
                {
                    Console.WriteLine("\n That's Not a Future Date!!\n\n");
                }

            }
        }
    }
}

bool checkString(string input)
{
    if (input == String.Empty)
    {
        Console.WriteLine("Input cannot be null");
        return false;
    }
    return true;
}

UserAge calcAge(DateTime today, DateTime birthDate)
{
    int ageYears = today.Year - birthDate.Year;
    int ageMonths = today.Month - birthDate.Month;
    int ageDays = today.Day - birthDate.Day;

    if (ageDays < 0)
    {
        ageMonths--;
        int lastDayPrevMonth = DateTime.DaysInMonth(today.Year, today.Month - 1);
        ageDays += lastDayPrevMonth;
    }

    if (ageMonths < 0)
    {
        ageYears--;
        ageMonths += 12;
    }

    return new UserAge(ageYears, ageMonths, ageDays);
}

UserAge calcAgeInDaysAndMonths(UserAge userAge, DateTime today, DateTime birthDate)
{
    int years = today.Year - birthDate.Year;
    int userYears = (today.Month >= birthDate.Month) ? years : years - 1;
    int userDays = (int)today.Subtract(birthDate).TotalDays;
    int userMonths = userDays / 30;

    UserAge updatedUserAge = new UserAge(userAge.ageYears, userAge.ageMonths, userAge.ageDays);
    updatedUserAge.ageInDays = userDays;
    updatedUserAge.ageInMonths = userMonths;
    return updatedUserAge;
}

UserAge calcAgeInPlanets(UserAge userAge)
{
    userAge.ageInPlanets.Add(Planets.Mercury, userAge.ageYears * 0.240);
    userAge.ageInPlanets.Add(Planets.Venus, userAge.ageYears * 0.615);
    userAge.ageInPlanets.Add(Planets.Earth, userAge.ageYears);
    userAge.ageInPlanets.Add(Planets.Mars, userAge.ageYears * 1.880);
    userAge.ageInPlanets.Add(Planets.Jupiter, userAge.ageYears * 11.863);
    userAge.ageInPlanets.Add(Planets.Saturn, userAge.ageYears * 29.447);
    userAge.ageInPlanets.Add(Planets.Uranus, userAge.ageYears * 84.017);
    userAge.ageInPlanets.Add(Planets.Neptune, userAge.ageYears * 164.791);
    userAge.ageInPlanets.Add(Planets.Pluto, userAge.ageYears * 248.090);
    return userAge;
}

void dateOutputFormatDaysAndMonths(UserAge userAge)
{
    bool loop = true;
    int choice;

    while (loop)
    {
        subMenu(true);
        if (int.TryParse(Console.ReadLine(), out choice))
        {
            switch (choice)
            {
                case 0: loop = false; break;
                case 1: Console.WriteLine("\nUser has {0} years\n\n", userAge.ageYears); break;
                case 2: Console.WriteLine("\nUser has {0} years or {1} months\n\n", userAge.ageYears, userAge.ageInMonths); break;
                case 3: Console.WriteLine("\nUser has {0} years or {1} days\n\n", userAge.ageYears, userAge.ageInDays); break;
                case 4: Console.WriteLine("\nUser has {0} years, {1} months, or {2} days\n\n", userAge.ageYears, userAge.ageInMonths, userAge.ageInDays); break;
                default: Console.WriteLine("\nNon-valid Option\n\n"); break;
            }
        }
        else
            Console.WriteLine("Non-valid Choice\n\n");
    }

}

void dateOutputFormat(UserAge userAge)
{
    bool loop = true;
    int choice;

    while (loop)
    {
        subMenu(false);
        if (int.TryParse(Console.ReadLine(), out choice))
        {
            switch (choice)
            {
                case 0: loop = false; break;
                case 1: Console.WriteLine("\nUser has {0} years\n\n", userAge.ageYears); break;
                case 2: Console.WriteLine("\nUser has {0} years and {1} months\n\n", userAge.ageYears, userAge.ageMonths); break;
                case 3: Console.WriteLine("\nUser has {0} years and {1} days\n\n", userAge.ageYears, userAge.ageDays); break;
                case 4: Console.WriteLine("\nUser has {0} years, {1} months, and {2} days\n\n", userAge.ageYears, userAge.ageMonths, userAge.ageDays); break;
                default: Console.WriteLine("\nNon-valid Option\n\n"); break;
            }
        }
        else
            Console.WriteLine("Non-valid Choice\n\n");
    }

}

void datePlanetOutput()
{
    bool loop = true;
    int choice;

    while (loop)
    {
        subMenuPlanets();
        if (int.TryParse(Console.ReadLine(), out choice))
        {
            switch (choice)
            {
                case (9): loop = false; break;
                case (int)Planets.Mercury: planetOutputList("Mercury", choice); break;
                case (int)Planets.Venus: planetOutputList("Venus", choice); break;
                case (int)Planets.Earth: planetOutputList("Earth", choice); break;
                case (int)Planets.Mars: planetOutputList("Mars", choice); break;
                case (int)Planets.Jupiter: planetOutputList("Jupiter", choice); break;
                case (int)Planets.Saturn: planetOutputList("Saturn", choice); break;
                case (int)Planets.Uranus: planetOutputList("Uranus", choice); break;
                case (int)Planets.Neptune: planetOutputList("Neptune", choice); break;
                case (int)Planets.Pluto: planetOutputList("Pluto", choice); break;
                default: Console.WriteLine("\nNon-valid Option\n\n"); break;
            }
        }
        else
            Console.WriteLine("Non-valid Choice\n\n");
    }
}


void planetOutputList(String planet, int choice)
{
    int i = 1;
    foreach (var item in users)
    {
        Console.WriteLine("\nUser {0} Will Have {1} Years in {2}\n", i++,item.ageInPlanets[(Planets)choice], planet);
    }
}
