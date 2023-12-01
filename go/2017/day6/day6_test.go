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

	var expected int = 5

	if got != expected {
		t.Errorf("PartOne = %d; want %d", got, expected)
	}
}

func TestPartOne_Real(t *testing.T) {
	b, err := os.ReadFile("input.txt")
	if err != nil {
		return
	}

	input := string(b)

	got := part1(input)

	var expected int = 11137

	if got != expected {
		t.Errorf("PartOne = %d; want %d", got, expected)
	}
}

func TestPartTwo_Test(t *testing.T) {
	b, err := os.ReadFile("test_input.txt")
	if err != nil {
		return
	}

	input := string(b)

	got := part2(input)

	var expected int = 4

	if got != expected {
		t.Errorf("PartTwo = %d; want %d", got, expected)
	}
}

func TestPartTwo_Real(t *testing.T) {
	b, err := os.ReadFile("input.txt")
	if err != nil {
		return
	}

	input := string(b)

	got := part2(input)

	var expected int = 1037

	if got != expected {
		t.Errorf("Part2 %d;  want %d", got, expected)
	}
}
