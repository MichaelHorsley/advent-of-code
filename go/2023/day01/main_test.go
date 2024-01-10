package main

import (
	"os"
	"testing"
)

func TestPartOne_WithTestInput(t *testing.T) {
	b, err := os.ReadFile("test_input.txt")
	if err != nil {
		return
	}

	input := string(b)

	got := part1(input)

	var expected int = 142

	if got != expected {
		t.Errorf("PartOne = %d; want %d", got, expected)
	}
}

func TestPartOne_WithRealInput(t *testing.T) {
	b, err := os.ReadFile("input.txt")
	if err != nil {
		return
	}

	input := string(b)

	got := part1(input)

	var expected int = 54634

	if got != expected {
		t.Errorf("PartOne = %d; want %d", got, expected)
	}
}

func TestPartTwo_Test(t *testing.T) {
	b, err := os.ReadFile("test_input_part_2.txt")
	if err != nil {
		return
	}

	input := string(b)

	got := part2(input)

	var expected int = 281

	if got != expected {
		t.Errorf("Part2 %d; want %d", got, expected)
	}
}

func TestPartTwo_Real(t *testing.T) {
	b, err := os.ReadFile("input.txt")
	if err != nil {
		return
	}

	input := string(b)

	got := part2(input)

	var expected int = 53855

	if got != expected {
		t.Errorf("Part2 %d;  want %d", got, expected)
	}
}
