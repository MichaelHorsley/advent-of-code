package main

import (
	"fmt"
	"os"
	"strconv"
	"strings"
)

func main() {

	b, err := os.ReadFile("input.txt")
	if err != nil {
		return
	}

	input := string(b)

	fmt.Println(part1(input))
	fmt.Println(part2(input))
}

type Node struct {
	name        string
	parent      *Node
	children    []*Node
	weight      int
	totalWeight int
}

func (n *Node) calculateTotalWeight() int {

	var sum int = 0

	for _, v := range n.children {
		sum += v.calculateTotalWeight()
	}

	n.totalWeight = n.weight + sum

	return n.totalWeight
}

func (n *Node) areTheKidsOkay() bool {
	weight := make(map[int][]*Node)

	for _, child := range n.children {
		weight[child.totalWeight] = copyAndExpandList(weight[child.totalWeight], child)
	}

	if len(weight) <= 1 {
		return true
	} else {
		return false
	}
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

func part2(input string) (res int) {
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
				newNode.weight = GetWeightFromString(sections[1])
				newNode.children = CreateChildren(allNodeMap, newNode, sections[3:])

				allNodeMap[key] = newNode
			} else {
				existingNode.weight = GetWeightFromString(sections[1])
				existingNode.children = CreateChildren(allNodeMap, existingNode, sections[3:])
			}
		} else {
			sections := strings.Split(node, " ")
			key := sections[0]
			existingNode, containsNode := allNodeMap[key]

			if !containsNode {
				newNode := new(Node)
				newNode.name = key
				newNode.weight = GetWeightFromString(sections[1])

				allNodeMap[key] = newNode
			} else {
				existingNode.weight = GetWeightFromString(sections[1])
			}
		}
	}

	for _, startingNode := range allNodeMap {
		if startingNode.parent == nil {

			startingNode.calculateTotalWeight()

			imbalancedNode := findImbalancedProgramme(startingNode)

			weightDictionary := make(map[int][]*Node)

			for _, child := range imbalancedNode.children {
				weightDictionary[child.totalWeight] = copyAndExpandList(weightDictionary[child.totalWeight], child)
			}

			var incorrectWeight int = 0
			var correctWeight int = 0

			for k, v := range weightDictionary {
				if len(v) > 1 {
					correctWeight = k
				} else {
					incorrectWeight = k
				}
			}

			weightDelta := correctWeight - incorrectWeight
			x1 := weightDictionary[incorrectWeight][0]
			imbalancedNodeWeight := x1.weight

			return imbalancedNodeWeight + weightDelta

		}
	}

	return 0
}

func findImbalancedProgramme(parentNode *Node) *Node {
	weightDictionary := make(map[int][]*Node)

	for _, child := range parentNode.children {
		weightDictionary[child.totalWeight] = copyAndExpandList(weightDictionary[child.totalWeight], child)
	}

	if len(weightDictionary) == 1 {
		return parentNode.parent
	} else {
		var incorrectWeight int = 0

		for k, v := range weightDictionary {
			if len(v) > 1 {

			} else {
				incorrectWeight = k
			}
		}

		return findImbalancedProgramme(weightDictionary[incorrectWeight][0])
	}
}

func GetWeightFromString(weightAsString string) (res int) {
	value, err := strconv.Atoi(strings.Replace(strings.Replace(weightAsString, "(", "", 1), ")", "", 1))

	if err != nil {
		panic("Balls")
	}

	return int(value)
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
