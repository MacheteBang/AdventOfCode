package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
	"sort"
	"strconv"
)

func main() {
	inputFile, err := os.Open("input.txt")

	if err != nil {
		log.Fatal(err)
	}

	defer inputFile.Close()

	scanner := bufio.NewScanner(inputFile)

	currentCalorieCount := 0
	elves := []int{}

	for scanner.Scan() {
		if scanner.Text() == "" {
			elves = append(elves, currentCalorieCount)
			currentCalorieCount = 0
		} else {
			intValue, err := strconv.Atoi(scanner.Text())
			if err != nil {
				fmt.Println(err)
			}

			currentCalorieCount += intValue

		}
	}

	sort.Sort(sort.Reverse(sort.IntSlice(elves)))

	// fmt.Println(elves[0])
	// fmt.Println(elves[1])
	// fmt.Println(elves[2])

	fmt.Println("Sum of Top 3 is:", (elves[0] + elves[1] + elves[2]))

}
