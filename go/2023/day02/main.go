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

func part1(input string) (res int) {
	lines := strings.Split(input, "\r\n")

	maxCubes := map[string]int{
		"blue":  14,
		"red":   12,
		"green": 13,
	}

	sum := 0

	for _, line := range lines {
		gameString := strings.Split(line, ": ")[0]
		cubeNumbers := strings.Split(line, ": ")[1]
		validGame := true

		hands := strings.Split(cubeNumbers, "; ")

		for _, hand := range hands {
			cubeCombinations := strings.Split(hand, ", ")

			for _, combination := range cubeCombinations {
				cubeNumber, _ := strconv.Atoi(strings.Split(combination, " ")[0])
				cubeColour := strings.Split(combination, " ")[1]

				if cubeNumber > maxCubes[cubeColour] {
					validGame = false
				}
			}
		}

		gameNumber, _ := strconv.Atoi(strings.Split(gameString, " ")[1])

		if validGame {
			sum += gameNumber
		}
	}

	return sum
}

func part2(input string) (res int) {
	lines := strings.Split(input, "\r\n")

	sum := 0

	for _, line := range lines {

		gameCubeLowestNumbers := make(map[string]int)

		cubeNumbers := strings.Split(line, ": ")[1]

		hands := strings.Split(cubeNumbers, "; ")

		for _, hand := range hands {
			cubeCombinations := strings.Split(hand, ", ")

			for _, combination := range cubeCombinations {
				cubeNumber, _ := strconv.Atoi(strings.Split(combination, " ")[0])
				cubeColour := strings.Split(combination, " ")[1]

				v, ok := gameCubeLowestNumbers[cubeColour]

				if ok {
					if cubeNumber > v {
						gameCubeLowestNumbers[cubeColour] = cubeNumber
					}
				} else {
					gameCubeLowestNumbers[cubeColour] = cubeNumber
				}
			}
		}

		gameHand := 1
		for _, value := range gameCubeLowestNumbers {
			gameHand *= value
		}

		sum += gameHand
	}

	return sum
}
