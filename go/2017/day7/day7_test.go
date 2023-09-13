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
		t.Errorf("PartOne = %s; want %s", got, expected)
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

func TestPartTwo_Test(t *testing.T) {
	b, err := os.ReadFile("test_input.txt")
	if err != nil {
		return
	}

	input := string(b)

	got := part2(input)

	var expected int = 60

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

	var expected int = 391

	if got != expected {
		t.Errorf("Part2 %d;  want %d", got, expected)
	}
}
