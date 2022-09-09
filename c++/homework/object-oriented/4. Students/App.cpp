#include "App.h"

#define space std::cout <<"\n\n" // Двойной перенос строки для удобства

#define demo demoGroup // Имя объекта для демонстрации

#define INDEX 48 // Номер единицы в табице чаров

// std::cout << std::fixed << std::setprecision(AMOUNT_OF_MARKS_AFTER_COMA) << value;




void App::run()
{

    // Ограничивает вывод вещественных чисел до первого знака после запятой
    std::cout << std::fixed << std::setprecision(1);


#pragma region PREPARATIONS


    // Новый Объект
    Group demoGroup("New");

    // Создание группы из случайных учеников, имена берутся из файла "NamePool.txt"
    demoGroup.setRandomStudentList();
    
    // Создание случайного списка предметов для группы
    demoGroup.setRandomSubjectList();

    // Объединение всех учеников под общим списком предметов.
    // Опционально. Можно задать список дисциплин каждому ученику отдельно.
    demoGroup.bind();

    // Заполнение списка оценок каждого ученика случайными оценками от 1 до 12
    demoGroup.setRandomGradesForEveryone();


#pragma endregion PREPARATIONS 



 #pragma region MENU_LOOP

    int i = 0;

    char c_cont = 'y';

    while (c_cont != 'n' && c_cont != 'N')
    {
        drawMenuStart();
        i = selector(5);

        switch (i)
        {
        case 1:
            showGrades(demo);
            break;
        case 2:
            showAvg(demo);
            break;
        case 3:
            showAvgForSubjects(demo);
            break;
        case 4:
            showGroupAvg(demo);
            break;
        case 5:
            showMinMax(demo);
            break;
        default:
            break;
        }
        showContinue();
        c_cont = _getwch();
        system("cls");
    }

    std::cout << "\t\tYou've quit! Have a nice day!\n\t\t(Press any key) .....";
    space;

 #pragma endregion MENU_LOOP


 
}




void App::showContinue()
{
    const int len = 47;
    std::cout << "\t" << char(201);
    for (int i = 0; i < len; i++)
    {
        std::cout << char(205);
    }
    std::cout << char(187);
    std::cout << "\n\t" << char(186);

    std::cout << "  Do you want to continue the programme? (y/n) ";

    std::cout << char(186) << "\n";
    std::cout << "\t" << char(200);
    for (int i = 0; i < len; i++)
    {
        std::cout << char(205);
    }
    std::cout << char(188) << "\n";
}

void App::drawMenuStart()
{
    std::cout << "Choose an action:";
    space;
    std::cout << "1. Students' grades\n";
    std::cout << "2. Average grades\n";
    std::cout << "3. Average grades (for all subjects)\n";
    std::cout << "4. Average grade for a group\n";
    std::cout << "5. Group min and max grades (for all subjects)";
    space;
}

int App::selector(int nOptionsQuantity)
{
    char user;
    do
    {
        user = _getwch();
    } while ((int)user < INDEX && (int)user > INDEX + nOptionsQuantity); // Пока ввод пользователя меньше 1 или больше кол-ва вариантов в меню

    return (int)user-INDEX;

}



void App::showGrades(Group& demo)
{
    space;
    space;

    for (int i = 0; i < demo.getStudentCounter(); i++)
    {
        std::cout << demo.getCrewPointer()[i].getName();
        space;
        for (int j = 0; j < demo.getCrewPointer()[i].getSubjectCounter(); j++)
        {
            std::cout << "\t" << demo.getCrewPointer()[i].getSubjectsPointer()[j].getName() << "\t\t";
            if (demo.getCrewPointer()[i].getSubjectsPointer()[j].getName() == "Literature" || demo.getCrewPointer()[i].getSubjectsPointer()[j].getName() == "Chemistry") std::cout << "\t";
            if (demo.getCrewPointer()[i].getSubjectsPointer()[j].getName() != "Literature" && demo.getCrewPointer()[i].getSubjectsPointer()[j].getName() != "Chemistry" && demo.getCrewPointer()[i].getSubjectsPointer()[j].getName() != "Computer Science") std::cout << "\t\t";
            for (int k = 0; k < demo.getCrewPointer()[i].getGradeCounters()[j]; k++)
            {
                std::cout << demo.getCrewPointer()[i].getGrades()[j][k] << " ";
            }
            space;
        }
        space;
        space;
    }

    space;
    space;
}

void App::showGroupAvg(Group& demo)
{
    space;
    space;
    std::cout << "\t\tGroup score: " << demo.getGroupAverageGrade();
    space;
    space;
}

void App::showAvg(Group& demo)
{
    space;
    space;
    for (int i = 0; i < demo.getStudentCounter(); i++)
    {
        std::cout << demo.getCrewPointer()[i].getName() << "\t\t";
        std::cout << demo.getStudentsAverageGrades()[i] << "\n";
    }

    space;
    space;
}

void App::showAvgForSubjects(Group& demo)
{
    space;
    space;
    for (int i = 0; i < demo.getStudentCounter(); i++)
    {
        std::cout << demo.getCrewPointer()[i].getName() << "\t";
        for (int j = 0; j < demo.getCrewPointer()[i].getSubjectCounter(); j++)
        {

            std::cout << demo.getStudentsAverageGradesForSubjects()[i][j] << " ";

        }
        std::cout << "\t";
        std::cout << demo.getStudentsAverageGrades()[i];
        std::cout << "\n";
    }

    space;
    space;
}

void App::showMinMax(Group& demo)
{
    space;
    space;
    for (int i = 0; i < demo.getSubjectCounter(); i++)
    {
        std::cout << "\t";
        std::cout << demo.getSubjectListPointer()[i].getName() << "\t";
        if (demo.getSubjectListPointer()[i].getName() == "Literature" || demo.getSubjectListPointer()[i].getName() == "Chemistry") std::cout << "\t";
        if (demo.getSubjectListPointer()[i].getName() != "Literature" && demo.getSubjectListPointer()[i].getName() != "Chemistry" && demo.getSubjectListPointer()[i].getName() != "Computer Science") std::cout << "\t\t";
        std::cout << "Max = " << demo.MaxMin()[i][0] << " (" << demo.getCrewPointer()[demo.MaxMin()[i][2]].getName() << ")" << "   " << " Min = " << demo.MaxMin()[i][1] << " (" << demo.getCrewPointer()[demo.MaxMin()[i][3]].getName() << ")" << "\n";
    }
    space;
    space;
}
