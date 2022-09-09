#ifndef SPECIES_H
#define SPECIES_H


namespace SpeciesEnum
{

	// Биологический вид растения; (the order of the elements is represented as it is in the excel table)
	enum Species
	{

		PetuniaPendula,					// Петуния ампельная;
		PetuniaAtkinsiana,				// Петуния обыкновенная;
		Geranium,						// Герань;
		Fuchsia,						// Фуксия;
		Cineraria,						// Цинерария;
		Hyacinthus,						// Гиацинт;
		Viola,							// Виола;
		PetuniaPendulaCachepot,			// Петуния ампельная кашпо;
		Verbena,						// Вербена;
		Tagetes,						// Тагетес, он же Бархатец;
		Antirrhinum,					// Львиный зев;
		Osteospermum,					// Остеоспермум;
		Dahlia,							// Георгин;
		Calibrachoa,					// Калибрахоа;
		Salvia,							// Сальвия;
		Catharanthus,					// Катарантус;

	};

		static const Species All[] = { PetuniaPendula,
									   PetuniaAtkinsiana,
									   Geranium,
									   Fuchsia,
									   Cineraria,
									   Hyacinthus,
									   Viola,
									   PetuniaPendulaCachepot,
									   Verbena,
									   Tagetes,
									   Antirrhinum,
									   Osteospermum,
									   Dahlia,
									   Calibrachoa,
									   Salvia,
									   Catharanthus };

	
}



#endif
