using System;

class MainClass {
  public static void Main (string[] args) {
    int mazeSize = 8; 
    int initialX = 7; 
    int initialY = 0; 
    RandomMaze randomMaze = new RandomMaze(mazeSize,initialX,initialY); 
    Micromouse micromouse = new Micromouse(mazeSize,initialX,initialY,randomMaze.getMaze());
    Console.WriteLine("Locate Center"); 
  }
}