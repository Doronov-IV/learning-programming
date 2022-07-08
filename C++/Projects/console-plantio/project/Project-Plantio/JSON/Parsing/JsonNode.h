#ifndef JSON_NODE_H
#define JSON_NODE_H


#include <map>
#include <iostream>
#include <string>


using std::string;
using std::map;


namespace JsonParsing
{


	// Класс "Нода";
	class JsonNode
	{


	public:

		virtual ~JsonNode();

		void debugPrint(size_t depth) const;

	//private:

		string sName;

		map<string, JsonNode*> objects;

		map<string, JsonNode*> arrays;

		map<string, string> strings;

		map<string, double> numbers;

		map<string, bool> flags;

		bool isArray = false;

		bool isNull = false;


	private:

		void printStrings(size_t depth) const;

		void printNumbers(size_t depth) const;

		void printFlags(size_t depth) const;

		void printObjects(size_t depth) const;

		void printArrays(size_t depth) const;

		void printTab(size_t depth) const;

	};

}


#endif

