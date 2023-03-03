#include <ctime>
#include <iostream>
#include <conio.h>

using std::cout;
using std::cin;
using std::endl;
using std::rand;
using std::string;
using std::swap;

/*
   
   Programme "Airport";

   Brief:
   — You can land a one of 100 aircrafts generated randomly;
   — You can choose the type of an aircraft to be landed;
   — If the boxes are filled, the small planes would stack up in hangars;
   — If the aircraft departs – it goes back to the "pool";
   — If you choose "depart", the airport is printed so you can choose aircraft and type down its id. 

  "Free" – a reserved name for freespace containers;
   "1337" – a reserved id for freespace containers;
   "0" – a reserved size accordingly.

*/

/* GLOBAL VARIABLES */

const int BOX_Q = 5;
const int HANGAR_Q = 4;
const int BIG_SIZE = 100;

/* structures */

struct Aircraft {
    string name = "unknown";
    int id = 0;
    int size = 0;
};

struct VFA54 {
    static int squadCounter;
    Aircraft* formation = new Aircraft[squadCounter];

};

int VFA54::squadCounter = 100;

struct Airport {
    Aircraft* Box = new Aircraft[BOX_Q];
    Aircraft* Hangar = new Aircraft[HANGAR_Q * 2];
};


/* main */
void TaskOne();
void TaskTwo();
void airportTerminal(Airport& airport, VFA54& squad);
void createPort(Airport& airport);
void Land(Aircraft ship, Airport& airport);
void TakeOffById(int id, Airport& airport, VFA54& squad);
void displayPort(Airport& port);

VFA54 makeSquadron();
void showSquadron(VFA54 squad);
void addAircraft(Aircraft craft, VFA54 squad);

/* auxiliary */

void showContinue();
void showSubContinue(int);


int main() {
    srand(time(NULL));
    char user;
    char c_con;
    string menu = "\nChoose the number of a task (1-2).\n\nYou: ";

    do
    {
        cout << menu;
        user = _getch();

        switch (user)
        {
        case '1':
        {
            TaskOne();
            break;
        }
        case '2':
        {
            TaskTwo();
            break;
        }
        default:
            break;
        }
        showContinue();
        c_con = _getch();
        cout << "\n\n";
        system("cls");
    } while (c_con != 'n' && c_con != 'N');
    cout << "\n\n\tYou've quit. Have a nice day!\n\t   (Press any key...)\n\n\n";

    system("pause>0");
    return 0;
}



// main functions


void TaskOne() {
    cout << "\n\nTask One. Airport\n\n";

    Airport airport;

    createPort(airport);

    VFA54 SkullAndBones;

    SkullAndBones = makeSquadron();
    airportTerminal(airport, SkullAndBones);
}

void TaskTwo() {
    cout << "\n\nTask Two.\n\n";
    cout << "Error 404. Choose #y #1 to get to the airport manager.\n\n";
}

Aircraft createAircraft() {
    string name;
    int id;
    char size;
    Aircraft craft;
    cout << "\n\nSize (l/s ";
    size = _getch();
    switch (size) {
    case 'l':
        craft.size = 100;
        break;
    case 's':
        craft.size = 75;
        break;
    }
    cout << "\n\nName: ";
    cin >> name;
    craft.name = name;
    cout << "\n\nid : ";
    cin >> id;
    craft.id = id;
    return craft;
}

void createPort(Airport& airport) {
    for (int i = 0; i < BOX_Q; i++) {
        airport.Box[i].name = "Free";
        airport.Box[i].id = 1337;
        airport.Box[i].size = 0;
    }

    for (int i = 0; i < (HANGAR_Q * 2) - 1; i++) {
        airport.Hangar[i].name = "Free";
        airport.Hangar[i + 1].name = "Free";
        airport.Hangar[i].id = 1337;
        airport.Hangar[i + 1].id = 1337;
        airport.Hangar[i].size = 0;
        airport.Hangar[i + 1].size = 0;
    }
}

void Land(Aircraft ship, Airport& airport) {


    bool hangar_flag = false;
    bool hangar_box_flag = false;
    bool box_flag = false;

    for (int i = 0; i < HANGAR_Q * 2 - 1; i += 2) {
        if (airport.Hangar[i].id == 1337 && airport.Hangar[i + 1].id == 1337) {
            hangar_flag = true;
            break;
        }
    }

    for (int i = 0; i < HANGAR_Q * 2; i++) {
        if (airport.Hangar[i].id == 1337) {
            hangar_box_flag = true;
            break;
        }
    }

    if (hangar_flag == false) {
        cout << "\n\n\a [!] Warning, no room for the aircraft in hangars.\n\n";
    }

    if (hangar_box_flag == false) {
        cout << "\n\n\a [!] Warning, no room for the aircraft in hangars for small crafts.\n\n";
    }

    if (ship.size >= 100 && hangar_flag == false) {
        return;
    }


    for (int i = 0; i < BOX_Q; i++) {
        if (airport.Box[i].id == 1337) {
            box_flag = true;
            break;
        }
    }

    if (box_flag == false) {
        cout << "\n\n [!] Warning. No room for the aircraft in boxes.\n\n";
    }

    if (ship.size < 100 && box_flag == false && hangar_box_flag == false) {
        return;
    }

    if (ship.size >= 100 && hangar_flag == true) {
        for (int i = 0; i < HANGAR_Q * 2 - 1; i += 2) {
            if (airport.Hangar[i].id == 1337) {
                airport.Hangar[i] = ship;
                airport.Hangar[i + 1] = ship;
                break;
            }
        }
    }
    else if (ship.size < 100 && box_flag == true) {
        for (int i = 0; i < BOX_Q; i++) {
            if (airport.Box[i].id == 1337) {
                airport.Box[i] = ship;
                break;
            }

        }
    }
    else if (ship.size < 100 && box_flag == false && hangar_box_flag == true) {
        for (int i = 0; i < HANGAR_Q * 2; i++) {
            if (airport.Hangar[i].id == 1337) {
                airport.Hangar[i] = ship;
                break;
            }
        }
    }


}

void TakeOffById(int id, Airport& airport, VFA54& squad) {

    int address_box = 1337;
    int address_hangar = 1337;
    bool check = false;
    for (int i = 0; i < HANGAR_Q * 2; i++) {
        if ((airport.Hangar[i].id) == (id)) {
            check = true;
            address_hangar = i;
            break;
        }
    }
    for (int i = 0; i < BOX_Q; i++) {
        if ((airport.Box[i].id) == (id)) {
            check = true;
            address_box = i;
            break;
        }
    }
    if (id == 1337) check = false;
    if (check == false) {
        cout << "\n\n\a[!] Warning. No such aircraft found.\n\n";
        return;
    }
    else {
        if (address_box != 1337) {
            for (int i = 0; i < VFA54::squadCounter; i++) {
                if (squad.formation[i].id == 1337 && squad.formation[i].size == airport.Box[address_box].size) {
                    squad.formation[i] = airport.Box[address_box];
                }
            }
            airport.Box[address_box].name = "Free";
            airport.Box[address_box].id = 1337;
            airport.Box[address_box].size = 0;
        }
        if (address_hangar != 1337) {
            for (int i = 0; i < VFA54::squadCounter; i++) {
                if (squad.formation[i].id == 1337 && squad.formation[i].size == airport.Hangar[address_hangar].size) {
                    squad.formation[i] = airport.Hangar[address_hangar];
                }
            }
            if (airport.Hangar[address_hangar].size >= 100) {
                airport.Hangar[address_hangar].name = "Free";
                airport.Hangar[address_hangar + 1].name = "Free";
                airport.Hangar[address_hangar].id = 1337;
                airport.Hangar[address_hangar + 1].id = 1337;
                airport.Hangar[address_hangar].size = 0;
                airport.Hangar[address_hangar + 1].size = 0;
            }
            else {
                airport.Hangar[address_hangar].name = "Free";
                airport.Hangar[address_hangar].id = 1337;
                airport.Hangar[address_hangar].size = 0;
            }
        }
    }

}

void addAircraft(Aircraft craft, VFA54 squad) {
    VFA54::squadCounter++;
    squad.formation[VFA54::squadCounter - 1] = craft;
}

void displayPort(Airport& port) {
    cout << "\n\n\t<<< Airport Current State >>>\n\n";
    cout << "\tHangars:\n\n";

    for (int i = 0; i < HANGAR_Q * 2-1; i+=2) {
        if (port.Hangar[i].name != "Free" || port.Hangar[i+1].name != "Free") {
            if (port.Hangar[i].size >= 100) {
                cout << "\nName: " << port.Hangar[i].name;
                cout << "\nid: " << port.Hangar[i].id << "\n";
            }
            else {
                if (port.Hangar[i].id != 1337 && port.Hangar[i+1].id == 1337) {
                    cout << "\nName: " << port.Hangar[i].name;
                    cout << "\nid: " << port.Hangar[i].id << "\n";
                    cout << "\n{Free small aircraft space}\n";
                }
                else if (port.Hangar[i].id != 1337 && port.Hangar[i + 1].id != 1337) {
                    cout << "\nName: " << port.Hangar[i].name;
                    cout << "\nid: " << port.Hangar[i].id << "\n";
                    cout << "\nName: " << port.Hangar[i+1].name;
                    cout << "\nid: " << port.Hangar[i+1].id << "\n";
                }
                else if (port.Hangar[i].id == 1337 && port.Hangar[i+1].id != 1337) {
                    cout << "\n{Free small aircraft space}\n";
                    cout << "\nName: " << port.Hangar[i+1].name;
                    cout << "\nid: " << port.Hangar[i+1].id << "\n";
                }
            }
        }
        else {
            cout << "\n{Free Hangar}\n";
        }
    }

    cout << "\n\n\tBoxes:\n\n";

    for (int i = 0; i < BOX_Q; i++) {
        if (port.Box[i].name != "Free") {
            cout << "\nName: " << port.Box[i].name;
            cout << "\nid: " << port.Box[i].id << "\n";
        }
        else {
            cout << "\n{Free Box}\n";
        }
    }
    //cout << "\n\n";
}

// auxiliary

void showContinue() {
    const int len = 47;
    cout << "\t" << char(201);
    for (int i = 0; i < len; i++) {
        cout << char(205);
    }
    cout << char(187);
    cout << "\n\t" << char(186);

    cout << "  Do you want to continue the programme? (y/n) ";

    cout << char(186) << "\n";
    cout << "\t" << char(200);
    for (int i = 0; i < len; i++) {
        cout << char(205);
    }
    cout << char(188) << "\n";
    cout << "\t You: ";
}

void showSubContinue(int task_number) {
    const int len = 47;
    cout << "\t" << char(218);
    for (int i = 0; i < len; i++) {
        cout << char(196);
    }
    cout << char(191);
    cout << "\n\t" << char(179);

    cout << "     Do you want to continue Task " << task_number << "? (y/n)     ";

    cout << char(179) << "\n";
    cout << "\t" << char(192);
    for (int i = 0; i < len; i++) {
        cout << char(196);
    }
    cout << char(217) << "\n";
    cout << "\t You: ";
}

VFA54 makeSquadron() {
    VFA54 squad;
    int f;
    int random_id = 240;
    for (int i = 0; i < 100; i++) {
        random_id += i;
        if (random_id == 1337) {
            random_id += 1;
        }
        f = 1 + rand() % 9;
        switch (f) {
        case 1:
            squad.formation[i].name = "F-14B Tomcat";
            squad.formation[i].id = random_id;
            squad.formation[i].size = 75;
            break;

        case 2:
            squad.formation[i].name = "F-15C Eagle";
            squad.formation[i].id = random_id;
            squad.formation[i].size = 80;
            break;
        case 3:
            squad.formation[i].name = "F-15D Eagle";
            squad.formation[i].id = random_id;
            squad.formation[i].size = 85;
            break;
        case 4:
            squad.formation[i].name = "F-16C Viper";
            squad.formation[i].id = random_id;
            squad.formation[i].size = 75;
            break;
        case 5:
            squad.formation[i].name = "F/A-18C Hornet";
            squad.formation[i].id = random_id;
            squad.formation[i].size = 50;
            break;
        case 6:
            squad.formation[i].name = "C-130B";
            squad.formation[i].id = random_id;
            squad.formation[i].size = 150;
            break;
        case 7:
            squad.formation[i].name = "AC-130";
            squad.formation[i].id = random_id;
            squad.formation[i].size = 160;
            break;
        case 8:
            squad.formation[i].name = "AC-130A";
            squad.formation[i].id = random_id;
            squad.formation[i].size = 160;
            break;
        case 9:
            squad.formation[i].name = "WC-130B";
            squad.formation[i].id = random_id;
            squad.formation[i].size = 160;
            break;
        case 10:
            squad.formation[i].name = "C-130H";
            squad.formation[i].id = random_id;
            squad.formation[i].size = 160;
            break;
        default:
            squad.formation[i].name = "Archangel";
            squad.formation[i].id = random_id;
            squad.formation[i].size = 199;
        }
    }
    return squad;
}

void showSquadron(VFA54 squad) {
    int lightCounter = 0, heavyCounter = 0;
    for (int i = 0; i < VFA54::squadCounter; i++) {
        if (squad.formation[i].size < 100 && squad.formation[i].id != 1337) {
            lightCounter++;
        }
        else if (squad.formation[i].size >= 100 && squad.formation[i].id != 1337) heavyCounter++;
    }
    int tempcounter = lightCounter + heavyCounter;
    cout << "\n\n";
    cout << "There are " << tempcounter << " aircrafts in the air at the monent, including: \n\nLight fighters " << char(196) << " " << lightCounter << "\nHeavy vehicles " << char(196) << " " << heavyCounter;
}

void airportTerminal(Airport& airport, VFA54& squad) {
    char user;
    char user_type;
    int user_id, len = 120;
    bool flag = false;
    bool flag6 = false;
    char con = 'n';
    cout << "\n\n";
    cout << "\tChoose an option.\n\n  1. Airport status\n  2. Squadron status\n  3. Land an aircraft\n  4. Depart an aircraft\n  5. Depart everyone\n  6. Exit\n\n\n ";
    user = _getch();
    cout << "\n\n";

    switch (user) {
    case '1':
        displayPort(airport);
        cout << "\n\n";
        for (int i = 0; i < len; i++) {
            cout << char(196);
        }
        cout << "\n\n";
        airportTerminal(airport, squad);
        break;
    case '2':
        showSquadron(squad);
        cout << "\n\n";
        for (int i = 0; i < len; i++) {
            cout << char(196);
        }
        cout << "\n\n";
        airportTerminal(airport, squad);
        break;
    case '3':
        cout << "Choose the type (large/small) of a vehice (l/s)\n\nYou: ";
        user_type = _getch();
        int r;
        if (user_type == 'l' || user_type == 'L') {
            for (int i = 0; i < 100; i++) {
                r = rand() % VFA54::squadCounter;
                if (squad.formation[r].size >= 100) break;
            }
            for (int i = 0; i < VFA54::squadCounter; i++) {
                if (i == r && squad.formation[i].size >= 100) {
                    if (squad.formation[i].id != 1337) {
                        Land(squad.formation[i], airport);
                        squad.formation[i].name = "Unavailable";
                        squad.formation[i].id = 1337;
                        break;
                    }
                }
            }

        }
        else if (user_type == 's' || user_type == 'S') {
            for (int i = 0; i < 100; i++) {
                r = rand() % VFA54::squadCounter;
                if (squad.formation[r].size < 100) break;
            }
            for (int i = 0; i < VFA54::squadCounter; i++) {
                if (i == r && squad.formation[i].size < 100 && squad.formation[i].id != 1337) {
                    Land(squad.formation[i], airport);
                    squad.formation[i].name = "Unavailable";
                    squad.formation[i].id = 1337;
                    break;
                }
            }

        }
        cout << "\n\n   >>Done.";
        cout << "\n\n";
        for (int i = 0; i < len; i++) {
            cout << char(196);
        }
        cout << "\n\n";
        airportTerminal(airport, squad);
        break;
    case '4':
        displayPort(airport);
        cout << "\n\nChoose the aircraft id.\n\nYou: ";
        cin >> user_id;
        flag = false;
        for (int i = 0; i < HANGAR_Q * 2; i++) {
            if (airport.Hangar[i].id == user_id) {
                TakeOffById(user_id, airport, squad);
                flag = true;
                break;
            }
        }
        if (flag == false) {
            for (int i = 0; i < BOX_Q; i++) {
                if (airport.Box[i].id == user_id) {
                    TakeOffById(user_id, airport, squad);
                    flag = true;
                    break;
                }
            }
        }
        if (flag == true) {
            cout << "\n\n   >>Done\n\n";
        }
        else {
            cout << "\n\n [!] There is no such aircraft. Please, check your data again.\n\n";
        }
        cout << "\n\n";
        for (int i = 0; i < len; i++) {
            cout << char(196);
        }
        cout << "\n\n";
        airportTerminal(airport, squad);
        break;
    case '5':
        for (int i = 0; i < HANGAR_Q * 2; i++) {
            if (airport.Hangar[i].id != 1337) {
                TakeOffById(airport.Hangar[i].id, airport, squad);
            }
        }
        for (int i = 0; i < BOX_Q; i++) {
            if (airport.Box[i].id != 1337) {
                TakeOffById(airport.Box[i].id, airport, squad);
            }
        }
        for (int i = 0; i < HANGAR_Q * 2; i++) {
            if (airport.Hangar[i].id != 1337) {
                flag6 = true;
                break;
            }
        }
        for (int i = 0; i < BOX_Q; i++) {
            if (airport.Box[i].id != 1337) {
                flag6 = true;
                break;
            }
        }
        if (flag6 == false) {
            cout << "\n\n   >>Done\n\n";
        }
        cout << "\n\n";
        for (int i = 0; i < len; i++) {
            cout << char(196);
        }
        cout << "\n\n";
        airportTerminal(airport, squad);
        break;
    case '6':
        cout << "\tHave a nice day!\n\n";
        return;
        break;
    default:
        cout << "\t Please choose a proper action.\n\n";
        cout << "\n\n";
        for (int i = 0; i < len; i++) {
            cout << char(196);
        }
        cout << "\n\n";
        airportTerminal(airport, squad);
        break;
    }
}

/*
 *   21.10.21 Objectives:
 * — make a unite counter – completed
 * — functional hangars – completed
 * 
*/