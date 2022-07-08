#include "JsonNode.h"



JsonParsing::JsonNode::~JsonNode()
{
	for (auto node : objects)
	{
		delete node.second;
	}

	for (auto node : arrays)
	{
		delete node.second;
	}
}


void JsonParsing::JsonNode::debugPrint(size_t depth) const
{
	printTab(depth);

	if (!isArray) std::cout << "{\n";
	else std::cout << "[\n";

	printStrings(depth);
	printNumbers(depth);
	printFlags(depth);
	printObjects(depth);
	printArrays(depth);
	printTab(depth);

	if (!isArray) std::cout << "}\n";
	else std::cout << "]\n";
}


void JsonParsing::JsonNode::printStrings(size_t depth) const
{
	size_t _depth = depth + 1;

	for (auto item : strings)
	{
		printTab(_depth);

		if (!isArray) std::cout << '\"' << item.first << "\": \"";

		std::cout << item.second << "\",\n";
	}
}

void JsonParsing::JsonNode::printNumbers(size_t depth) const
{
	size_t _depth = depth + 1;

	for (auto item : numbers)
	{
		printTab(_depth);

		if (!isArray) std::cout << '\"' << item.first << "\": ";

		std::cout << item.second << ",\n";
	}
}

void JsonParsing::JsonNode::printFlags(size_t depth) const
{
	size_t _depth = depth + 1;

	for (auto item : flags)
	{
		printTab(_depth);

		if (!isArray) std::cout << '\"' << item.first << "\": \"";

		std::cout << std::noboolalpha << item.second << ",\n";
	}
}

void JsonParsing::JsonNode::printObjects(size_t depth) const
{
	size_t _depth = depth + 1;

	for (auto item : objects)
	{
		printTab(_depth);

		if (!isArray) std::cout << '\"' << item.first << "\": ";

		std::cout << "\n";

		item.second->debugPrint(_depth);

		std::cout  << ",\n";
	}
}

void JsonParsing::JsonNode::printArrays(size_t depth) const
{
	size_t _depth = depth + 1;

	for (auto item : arrays)
	{
		printTab(_depth);

		if (!isArray) std::cout << '\"' << item.first << "\": ";

		std::cout << "\n";

		item.second->debugPrint(_depth);

		std::cout << ",\n";
	}
}

void JsonParsing::JsonNode::printTab(size_t depth) const
{
	for (size_t i = 0ULL; i < depth; i++)
	{
		std::cout << "  ";
	}
}
