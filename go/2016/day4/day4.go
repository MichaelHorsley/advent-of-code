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
	fmt.Println(part2(input))
}

func part1(input string) (res int) {
	listOfRooms := strings.Split(strings.Replace(strings.Replace(input, "]", "", -1), "[", "|", -1), "\r\n")

	for position := 0; position < len(listOfRooms); position++ {

		roomMap := make(map[string]int)
		roomInformation := listOfRooms[position]

		roomAndChecksum := strings.Split(roomInformation, "|")

		checksum := roomAndChecksum[1]
		roomDetails := roomAndChecksum[0]

		foo := strings.Split(roomDetails, "-")

		sectorIdAsString := foo[len(foo)-1]

		foo = foo[:len(foo)-1]

		for _, a := range foo {
			fmt.Println(a)
		}

		checksumArray := strings.Split(checksum, "")
		roomDetailsArray := strings.Split(roomDetails, "")

		for _, roomDetail := range roomDetailsArray {
			if roomDetail != "-" {
				roomMap[roomDetail]++
			}
		}

		keys := make([]string, 0, len(roomMap))

		for key := range roomMap {
			keys = append(keys, key)
		}

		fmt.Println(checksumArray)
		fmt.Println(roomDetailsArray)
		fmt.Println(roomMap)
		fmt.Println(keys)
		fmt.Println(sectorIdAsString)
	}

	return 0
}

func part2(input string) (res int) {
	return 0
}
