#ifndef JSON_PARSER_H
#define JSON_PARSER_H


#ifndef JSON_NODE_H
#include "JsonNode.h"
#endif


#ifndef using_parser
#define using_parser JsonParser* parser = new JsonParser()
#endif


namespace JsonParsing
{
	// Класс "Парсер";
	class JsonParser
	{


	public:

		JsonParser();

		virtual ~JsonParser();

		void parse(string ptrSource);

		void debugPrint() const;

		JsonNode* ptrRoot;

	private:

		string ptrSource;

		size_t nSize;

		size_t nCurrentIndex;

		void parseObject(JsonNode* ptrCurrentNode);

		void parseArray(JsonNode* ptrCurrentNode);

		string parseString();

		void parseString(JsonNode* ptrCurrentNode, string sName);

		double parseNumber();

		void parseNext(JsonNode* ptrParent, string sName);

		void assertSize();

		void incrementIndex();

		void goToNextSymbol();

	};


}


#endif

