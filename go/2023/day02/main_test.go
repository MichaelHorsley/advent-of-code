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

	var expected int = 8

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

	var expected int = 2283

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

	var expected int = 2286

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

	var expected int = 0

	if got != expected {
		t.Errorf("Part2 %d;  want %d", got, expected)
	}
}
