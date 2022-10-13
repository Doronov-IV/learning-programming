#pragma once
#include <iostream>

using namespace std;

struct Trait {
    string sName;        // the name of a card
    string sProperty;    // the property of a trait means what action does this card do;
    int nFoodCount;      // determins what amount of food does the trait increase or decrease in the Pool;
    bool bFacedown;      // is the card facedown or not;
    bool bTrigger;       // has the card got trigger or it is going to implement its property only in the next round;
};

struct Species {
    int nFood = 0;                      // current amount of food
    int nPopulation = 0;                // the population
    int nBodysize = 0;                  // the body size

    Trait* spc_traits = new Trait[3];   // a list of traits of a species

    bool bIsExtinct = false;            // extinction check
    int nNumber = 0;                    // a sequential number of a species of a player (is essential for the <---> traits) 
    int nTraitamount = 0;               // the amount of traits of a species counter
    bool isCarnivore = false;           // is carnivore or not
    int nFatTissue = 0;                 // fat tissue counter

};

struct Player {
    Species* pl_species = new Species[5];   // the actual array of species
    Trait* pl_traits = new Trait[10];       // the array of traits in player's possession
    int nScore;                             // the general score counter
    int nNumber;                            // the player's sequential number
    bool bIsAi;                             // determins whether the player ... you've got it
    bool bMarker;                           // whether they start the round

    string playstyle;                       // in case it's AI
    string nickname;                        // for the leaderboards or something
};

Species createNewSpecies() {
    Species newspc;

    newspc.nFood = 0;
    newspc.nPopulation = 1;
    newspc.nBodysize = 1;

    for (int i = 0; i < 3; i++) {
        newspc.spc_traits[i].sName = "None";
        newspc.spc_traits[i].nFoodCount = 0;
        newspc.spc_traits[i].sProperty = "None";
        newspc.spc_traits[i].bFacedown = false;
        newspc.spc_traits[i].bTrigger = false;
    }

    newspc.bIsExtinct = false;
    newspc.nNumber = 1;
    newspc.nTraitamount = 0;
    newspc.isCarnivore = false;
    newspc.nFatTissue = 0;

    return newspc;
}

Player createNewPlayer(string type, int number) {

    Player newplayer;
    if (type == "player") {

        newplayer.pl_species[0] = createNewSpecies(); // starting one free spiece

        // everything other got to be free untill player gets it 

        for (int i = 1; i < 5; i++) { // non-starting spieces
            newplayer.pl_species[i].nFood = 0;
            newplayer.pl_species[i].nPopulation = 0;
            newplayer.pl_species[i].nBodysize = 0;

            for (int j = 0; j < 3; j++) { // their traits
                newplayer.pl_species[i].spc_traits[j].sName = "None";
                newplayer.pl_species[i].spc_traits[j].nFoodCount = 0;
                newplayer.pl_species[i].spc_traits[j].sProperty = "None";
                newplayer.pl_species[i].spc_traits[j].bFacedown = false;
                newplayer.pl_species[i].spc_traits[j].bTrigger = false;

            }
        }

        for (int i = 0; i < 10; i++) { // player's current traits
            newplayer.pl_traits[i].sName = "None";
            newplayer.pl_traits[i].nFoodCount = 0;
            newplayer.pl_traits[i].sProperty = "None";
            newplayer.pl_traits[i].bFacedown = false;
            newplayer.pl_traits[i].bTrigger = false;
        }

        newplayer.bMarker = false;
        newplayer.nScore = 0;
        newplayer.nNumber = number;
        newplayer.nickname = "nameless";
        newplayer.bIsAi = false;
        newplayer.playstyle = "random";
    }
    else if (type == "AI") {
        newplayer.pl_species[0] = createNewSpecies();

        newplayer.pl_species[0] = createNewSpecies(); // starting one free spiece

        // everything other got to be free untill player gets it 

        for (int i = 1; i < 5; i++) { // non-starting spieces
            newplayer.pl_species[i].nFood = 0;
            newplayer.pl_species[i].nPopulation = 0;
            newplayer.pl_species[i].nBodysize = 0;

            for (int j = 0; j < 3; j++) { // their traits
                newplayer.pl_species[i].spc_traits[j].sName = "None";
                newplayer.pl_species[i].spc_traits[j].nFoodCount = 0;
                newplayer.pl_species[i].spc_traits[j].sProperty = "None";
                newplayer.pl_species[i].spc_traits[j].bFacedown = false;
                newplayer.pl_species[i].spc_traits[j].bTrigger = false;
            }
        }

        for (int i = 0; i < 15; i++) { // player's current traits
            newplayer.pl_traits[i].sName = "None";
            newplayer.pl_traits[i].nFoodCount = 0;
            newplayer.pl_traits[i].sProperty = "None";
            newplayer.pl_traits[i].bFacedown = false;
            newplayer.pl_traits[i].bTrigger = false;
        }

        newplayer.bMarker = false;
        newplayer.nScore = 0;
        newplayer.nNumber = 0;
        newplayer.nickname = "Bob";
        newplayer.bIsAi = true;
        newplayer.playstyle = "strategic";
    }
    return newplayer;
}

bool checkIfSpeciesHasThatTrait(Species& species, int number) {
    for (int i = 0; i < 3; i++) {
        if (species.spc_traits[i].sName == species.spc_traits[number].sName) return true;
    }
    return false;
}

bool checkIfPlayerHasThatSpecies(Player& player, int number) {
    if (player.pl_species[number].nPopulation != 0) return true;
    else return false;
}

bool checkIfPlayerHasThatTrait(Player& player, int n) {
    if (player.pl_traits[n].sName == "None") return false;
    else return true;
}

int playersTraitsTotal(Player& player) {
    int c = 0;
    for (int i = 0; i < 10; i++) {
        if (player.pl_traits[i].sName != "None") c++;
    }
    return c;
}

int playersSpeciesTotal(Player& player) {
    int c = 0;
    for (int i = 0; i < 5; i++) {
        if (player.pl_species[i].nPopulation != 0) c++;
    }
    return c;
}
