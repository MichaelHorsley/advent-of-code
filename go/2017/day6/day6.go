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
	memoryBankStrings := strings.Split(input, "\t")
	previousIterations := make(map[string]int)

	previousIterations[strings.Join(memoryBankStrings, "|")] = 1

	memoryBanks := make(map[int]int)

	for position, v := range memoryBankStrings {
		value, _ := strconv.Atoi(v)
		memoryBanks[position] = value
	}

	iterations := 0

	for {
		maxInt := 0

		for position := 0; position < len(memoryBanks); position++ {
			value := memoryBanks[position]
			if value > maxInt {
				maxInt = value
			}
		}

		key := 0
		blocksToTake := 0

		for position := 0; position < len(memoryBanks); position++ {
			value := memoryBanks[position]
			if value == maxInt {
				key = position
				blocksToTake = value

				memoryBanks[position] = 0

				break
			}
		}

		for blocksToTake > 0 {
			key++

			if key >= len(memoryBanks) {
				key = 0
			}

			memoryBanks[key]++

			blocksToTake--
		}

		currentSetup := make([]int, 0)

		for position := 0; position < len(memoryBanks); position++ {
			value := memoryBanks[position]

			currentSetup = copyAndExpandList(currentSetup, value)
		}

		x := strings.Trim(strings.Join(strings.Fields(fmt.Sprint(currentSetup)), "|"), "[]")

		_, ok := previousIterations[x]

		if ok {
			iterations++
			return iterations
		} else {
			iterations++
			previousIterations[x] = 1
		}
	}
}

func part2(input string) (res int) {
	memoryBankStrings := strings.Split(input, "\t")
	previousIterations := make(map[string]int)

	previousIterations[strings.Join(memoryBankStrings, "|")] = 0

	memoryBanks := make(map[int]int)

	for position, v := range memoryBankStrings {
		value, _ := strconv.Atoi(v)
		memoryBanks[position] = value
	}

	iterations := 0

	for {
		maxInt := 0

		for position := 0; position < len(memoryBanks); position++ {
			value := memoryBanks[position]
			if value > maxInt {
				maxInt = value
			}
		}

		key := 0
		blocksToTake := 0

		for position := 0; position < len(memoryBanks); position++ {
			value := memoryBanks[position]
			if value == maxInt {
				key = position
				blocksToTake = value

				memoryBanks[position] = 0

				break
			}
		}

		for blocksToTake > 0 {
			key++

			if key >= len(memoryBanks) {
				key = 0
			}

			memoryBanks[key]++

			blocksToTake--
		}

		currentSetup := make([]int, 0)

		for position := 0; position < len(memoryBanks); position++ {
			value := memoryBanks[position]

			currentSetup = copyAndExpandList(currentSetup, value)
		}

		x := strings.Trim(strings.Join(strings.Fields(fmt.Sprint(currentSetup)), "|"), "[]")

		value, ok := previousIterations[x]

		if ok {
			return iterations - value
		} else {
			previousIterations[x] = iterations
		}

		iterations++
	}
}

func copyAndExpandList(original []int, value int) (res []int) {
	t := make([]int, len(original)+1)
	copy(t, original)
	t[len(t)-1] = value
	return t
}
