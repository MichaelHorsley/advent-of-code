package main

import (
	"fmt"
	"os"
	"strings"
)

func main() {

	b, err := os.ReadFile("input.txt")
	if err != nil {
		return
	}

	input := string(b)

	fmt.Println(part1(input))
	// fmt.Println(part2(input))
}

type Node struct {
	name     string
	parent   *Node
	children []*Node
}

func part1(input string) (res string) {
	nodesAndCommands := strings.Split(input, "\r\n")

	allNodeMap := make(map[string]*Node)

	for position := 0; position < len(nodesAndCommands); position++ {
		node := nodesAndCommands[position]

		if strings.Contains(node, "->") {
			sections := strings.Split(node, " ")
			key := sections[0]

			existingNode, containsNode := allNodeMap[key]

			if !containsNode {
				newNode := new(Node)
				newNode.name = key
				newNode.children = CreateChildren(allNodeMap, newNode, sections[3:])

				allNodeMap[key] = newNode
			} else {
				existingNode.children = CreateChildren(allNodeMap, existingNode, sections[3:])
			}
		} else {
			sections := strings.Split(node, " ")
			key := sections[0]
			_, containsNode := allNodeMap[key]

			if !containsNode {
				newNode := new(Node)
				newNode.name = key

				allNodeMap[key] = newNode
			}
		}
	}

	for k, n := range allNodeMap {
		if n.parent == nil {
			return k
		}
	}

	return "!"
}

func CreateChildren(allNodes map[string]*Node, parentNode *Node, sections []string) (res []*Node) {
	cleanNames(sections)

	listOfNodes := make([]*Node, 0)

	for _, nodeName := range sections {

		existingNode, containsNode := (allNodes)[nodeName]

		if !containsNode {
			node := new(Node)
			node.name = nodeName
			node.parent = parentNode

			(allNodes)[nodeName] = node

			listOfNodes = copyAndExpandList(listOfNodes, node)
		} else {
			existingNode.parent = parentNode

			listOfNodes = copyAndExpandList(listOfNodes, existingNode)
		}
	}

	return listOfNodes
}

func cleanNames(sections []string) {
	for position, v := range sections {
		if strings.Contains(v, ",") {
			sections[position] = strings.Replace(v, ",", "", 1)
		}
	}
}

func copyAndExpandList(commands []*Node, node *Node) (res []*Node) {
	t := make([]*Node, len(commands)+1)
	copy(t, commands)
	t[len(t)-1] = node
	return t
}
