#include "Currency.h"




vector<wstring> Currency::getCurrencyList()
{
    wstring auxiliary;

    if (vCurrencyList.size() == 0)
    {
        if (sFileName != "null")
        {
            FileBufferedReader reader;
             bool bTempResult = reader.read(sFileName);
             if (bTempResult)
             {
                 for (size_t i = 0ULL; i < reader.getBuffer().size(); i++)
                 {
                     Toolbox::StringToWString(auxiliary, reader.getBuffer()[i]);
                     this->vCurrencyList.push_back(auxiliary);
                 }
                 sActiveCurrencyName = vCurrencyList[0];
             }
             //else throw "Incorrect filepath!";
        }
        //else throw "You haven't assigned filename yet!";
        
    }
    return this->vCurrencyList;
}


bool Currency::setActiveCurrency(wstring sCurrencyName)
{
    bool res = false;
    if (sCurrencyName != getActiveCurrency())
    {
        for (wstring currency : vCurrencyList)
        {
            if (currency == sCurrencyName)
            {
                res = true;
                sActiveCurrencyName = currency;
            }
        }
    }
    return res;
}


wstring Currency::getActiveCurrency()
{
    return this->sActiveCurrencyName;
}


bool Currency::setFileName(string sFileName)
{
    bool res = false;
    this->sFileName = sFileName;

    ifstream in;
    in.open(sFileName);

    if (in.is_open())
    {
        res = true;
        in.close();
    }

    return res;
}


string Currency::getFileName()
{
    return this->sFileName;
}


Currency* Currency::getInstance()
{
    if (nullptr == instance) instance = new Currency();
    
    return instance;
}


Currency* Currency::instance = nullptr;


Currency::Currency()
{
    sFileName = "null";
}


Currency::~Currency()
{
    vCurrencyList.clear();
}
