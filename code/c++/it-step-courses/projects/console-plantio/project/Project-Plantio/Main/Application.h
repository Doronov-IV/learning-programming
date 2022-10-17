#ifndef APPLICATION_H
#define APPLICATION_H


#include "../Visual/VisualPlantio.h"

#ifndef TOOLBOX_H
#include "../Misc/Toolbox.h"
#endif

#ifndef JSON_PARSER_H
#include "../JSON/Parsing/JsonParser.h"
#endif

#include "../Wrappers/PlantBuilder.h"


using namespace JsonParsing;
using namespace Toolbox;
using namespace std;


#pragma region README


/*

                                                                   README


        Программа представляет собой систему учёта и распределения
        ресурсов и оценкой рисков на частном производстве растений.

        Пользователю предлагается два варианта: первый – подключить
        свой json файл с уже готовой моделью набора ресурсов (или же
        создать её в консоли), второй – ознакомиться с актуальными
        программами по выращиванию растений и выбрать себе
        что-то подходящее.

        Пользователь вводит показатели своих ресурсов, программа по
        формулам рассчитывает выручку, риски и трудозатраты. Иначе,
        можно ничего не вводить и просто знакомиться с разными
        способами получения той или иной суммы денег.

        На данный момент планируется: чтение и запись в файлы (с возможностью
        создания там новых объектов) , использование наследования, паттернов
        проектирования, использование динамических структур данных, реализация
        меню/UI с учётом возможностей.



        GitHub:     https://github.com/Doronov-IV/Project-Plantio


*/


#pragma endregion README




// Класс "Приложение";
// Программа выполняется в методе Run();
class Application
{


public:


	//Запрашивает экземпляр приложения;
	static Application* getInstance();


	// Основной метод, запускающий приложение;
	void run();


private:


	// Экземпляр нашего приложения;
	static Application* instance;

	// Демо первого задания;
	void RunPlantio();

    // Тестовое пространство;
    void Debug();


	// Конструктор по умолчанию;
	Application();
   
	// Деструктор;
	virtual ~Application();


#pragma region MENU_DRAWING


	// Рисует меню действий;
	void drawMenuStart();


#pragma endregion MENU_DRAWING


};


#endif
