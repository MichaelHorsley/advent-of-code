package main

import (
	"os"
	"testing"
)

func TestPartOne_Test(t *testing.T) {
	b, err := os.ReadFile("test_input.txt")
	if err != nil {
		return
	}

	input := string(b)

	got := part1(input)

	var expected string = "tknk"

	if got != expected {
		t.Errorf("PartOne(%s) = %s; want %s", input, got, expected)
	}
}

func TestPartOne_Real(t *testing.T) {
	b, err := os.ReadFile("input.txt")
	if err != nil {
		return
	}

	input := string(b)

	got := part1(input)

	var expected string = "gmcrj"

	if got != expected {
		t.Errorf("PartOne = %s; want %s", got, expected)
	}
}

// func TestPartTwo_TestingTheEnds(t *testing.T) {
// 	input := "91212129"
// 	var expected int64 = 9

// 	got := part1(input)

// 	if got != expected {
// 		t.Errorf("PartOne(%s) = %d; want %d", input, got, expected)
// 	}
// }

// type testCase struct {
// 	input    string
// 	expected int64
// }

// var addTests = []testCase{
// 	{"1212", 6},
// 	{"1221", 0},
// 	{"123425", 4},
// 	{"123123", 12},
// 	{"12131415", 4},
// }

// func TestPartTwo(t *testing.T) {

// 	for _, test := range addTests {
// 		if output := part2(test.input); output != test.expected {
// 			t.Errorf("Output %d not equal to expected %d", output, test.expected)
// 		}
// 	}
// }
