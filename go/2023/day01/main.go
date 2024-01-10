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

	sum := 0

	for _, line := range lines {
		numbers := make([]int, 0)
		characters := strings.Split(line, "")
		for _, character := range characters {
			if number, err := strconv.Atoi(character); err == nil {
				numbers = append(numbers, number)
			}
		}

		lineNumber, _ := strconv.Atoi(fmt.Sprintf("%d%d", numbers[0], numbers[len(numbers)-1]))

		sum += lineNumber
	}

	return sum
}

func part2(input string) (res int) {

	input = strings.ReplaceAll(input, "one", "o1e")
	input = strings.ReplaceAll(input, "two", "t2o")
	input = strings.ReplaceAll(input, "three", "t3e")
	input = strings.ReplaceAll(input, "four", "f4r")
	input = strings.ReplaceAll(input, "five", "f5e")
	input = strings.ReplaceAll(input, "six", "s6x")
	input = strings.ReplaceAll(input, "seven", "s7n")
	input = strings.ReplaceAll(input, "eight", "e8t")
	input = strings.ReplaceAll(input, "nine", "n9e")

	lines := strings.Split(input, "\r\n")

	sum := 0

	for _, line := range lines {
		numbers := make([]int, 0)
		characters := strings.Split(line, "")
		for _, character := range characters {
			if number, err := strconv.Atoi(character); err == nil {
				numbers = append(numbers, number)
			}
		}

		lineNumber, _ := strconv.Atoi(fmt.Sprintf("%d%d", numbers[0], numbers[len(numbers)-1]))

		sum += lineNumber
	}

	return sum
}
