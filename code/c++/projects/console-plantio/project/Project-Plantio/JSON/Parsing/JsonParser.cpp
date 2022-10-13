#include "JsonParser.h"


JsonParsing::JsonParser::JsonParser()
{
	ptrRoot = nullptr;
}

JsonParsing::JsonParser::~JsonParser()
{
	if (nullptr != ptrRoot)
	{
		delete ptrRoot;
		ptrRoot = nullptr;
	}
}

void JsonParsing::JsonParser::parse(string ptrSource)
{
	this->ptrSource = ptrSource;

	nSize = ptrSource.length() + 1;

	nCurrentIndex = 0ULL;

	ptrRoot = nullptr;

	if (nullptr == ptrRoot)
	{
		ptrRoot = new JsonNode();

		ptrRoot->sName = "root";

		parseObject(ptrRoot);
	}
}

void JsonParsing::JsonParser::debugPrint() const
{
	if (nullptr != ptrRoot)
	{
		ptrRoot->debugPrint(NULL);
	}
}



void JsonParsing::JsonParser::parseObject(JsonNode* ptrCurrentNode)
{
	incrementIndex();

	while ('}' != ptrSource[nCurrentIndex])
	{
		goToNextSymbol();

		string sName = parseString();

		goToNextSymbol();

		parseNext(ptrCurrentNode, sName);

		goToNextSymbol();
	}

	incrementIndex();
}

string JsonParsing::JsonParser::parseString()
{
	if ('\"' != ptrSource[nCurrentIndex])
		throw "Unexpected symbol!";

	string str = "";

	incrementIndex();

	while ('\"' != ptrSource[nCurrentIndex])
	{
		str += ptrSource[nCurrentIndex];

		incrementIndex();
	}

	incrementIndex();

	return str;
}

void JsonParsing::JsonParser::parseString(JsonNode* ptrCurrentNode, string sName)
{
	string sValue = parseString();

	if ("true" == sValue || "True" == sValue || "TRUE" == sValue)
	{
		ptrCurrentNode->flags.emplace(sName, true); 
	}
	else if ("false" == sValue || "False" == sValue || "FALSE" == sValue)
	{
		ptrCurrentNode->flags.emplace(sName, false);
	}
	else if ("null" == sValue || "Null" == sValue || "NULL" == sValue)
	{
		JsonNode* node = new JsonNode();
		node->sName = sName;
		node->isNull = true;
		ptrCurrentNode->objects.emplace(sName, node);
	}
	else
	{
		ptrCurrentNode->strings.emplace(sName, sValue);
	}
}

double JsonParsing::JsonParser::parseNumber()
{
	string sValue;

	while (',' != ptrSource[nCurrentIndex] &&
		   ' ' != ptrSource[nCurrentIndex] &&
		   ']' != ptrSource[nCurrentIndex] &&
		   '}' != ptrSource[nCurrentIndex])
	{
		sValue += ptrSource[nCurrentIndex];

		incrementIndex();
	}

	return stod(sValue);
}

void JsonParsing::JsonParser::parseArray(JsonNode* ptrCurrentNode)
{
	incrementIndex();

	size_t n = 0ULL;

	while (']' != ptrSource[nCurrentIndex])
	{
		goToNextSymbol();

		string sName = std::to_string(n);

		n += 1;

		goToNextSymbol();

		parseNext(ptrCurrentNode, sName);

		goToNextSymbol();
	}

	incrementIndex();
}

void JsonParsing::JsonParser::parseNext(JsonNode* ptrParent, string sName)
{
	switch (ptrSource[nCurrentIndex])
	{
	case '{': {

		JsonNode* node = new JsonNode();
		node->sName = sName;
		ptrParent->objects.emplace(sName, node);
		parseObject(node);

	}break;
	case '[': {

		JsonNode* node = new JsonNode();
		node->sName = sName;
		node->isArray = true;
		ptrParent->arrays.emplace(sName, node);
		parseArray(node);

	}break;
	case '\"': {

		parseString(ptrParent, sName);

	}break;
	default: {

		double dValue = parseNumber();
		ptrParent->numbers.emplace(sName, dValue);

	}break;
	}
}


void JsonParsing::JsonParser::assertSize()
{
	if (nCurrentIndex > nSize) throw "Index is out of bounds!";
}

void JsonParsing::JsonParser::incrementIndex()
{
	nCurrentIndex += 1;

	assertSize();
}

void JsonParsing::JsonParser::goToNextSymbol()
{
	while (' '  == ptrSource[nCurrentIndex]  ||
		   '\n' == ptrSource[nCurrentIndex]  || 
		   '\r' == ptrSource[nCurrentIndex]  ||
		   ':'  == ptrSource[nCurrentIndex]  ||
		   ','  == ptrSource[nCurrentIndex]	 ||
		   '\t' == ptrSource[nCurrentIndex])
	{
		incrementIndex();
	}
}