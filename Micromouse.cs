using System; 
using System.Collections.Generic; 

class Micromouse{
  private int mazeSize; 
  private int currentX; 
  private int currentY;
  private Maze mmMaze;  
  private char currentDir; 

  public Micromouse(int mazeSize, int initialX, int initialY, Maze realMaze){
    Console.WriteLine("Hi! I'm a micromouse");
    this.mazeSize = mazeSize; 
    this.currentX = initialX; 
    this.currentY = initialY; 
    startingMaze();
    explore(realMaze); 
    mmMaze.printMazeWithValues(currentX,currentY);
  }


  public void startingMaze(){
    mmMaze = new Maze(mazeSize);
    if(currentX == 0){
      currentDir = 's';
      addLeftWall(); 
      addRightWall();
    }else{
      currentDir = 'n';
      addLeftWall(); 
      addRightWall();  
    }
  }


  public bool explore(Maze realMaze){
    Console.WriteLine("Start of exploration");
    int stop = 0 ;
    while(mmMaze.getCell(currentX,currentY).getValue() != 0 ){
      Console.WriteLine("current Iteration " + stop);
      Console.WriteLine("current pos (" +currentX+","+currentY+")"  ); 
      mmMaze.printMazeWithValues(currentX,currentY);
   
      mmMaze.printMaze();
      realMaze.printMaze(); 
      stop++; 
      bool newWalls = registerWall(realMaze); 
      if(newWalls){
        updateValues(); 
      }
      char newDir = getNextMove(); 

      if(currentDir != newDir){
        turn(newDir);
      }
      move(); 
    }
    return true;
  }

  public bool registerWall(Maze realMaze){
    bool foundWalls = false; 
    bool[] realMazeWalls = realMaze.getCell(currentX,currentY).getWalls();  // N S E W 
    switch(currentDir){
      case 'n':
        if(realMazeWalls[0]){ // north 
          addFrontWall();
          foundWalls = true; 
        }
        if(realMazeWalls[2]){ // east 
          addRightWall();
          foundWalls = true; 
        }
        if(realMazeWalls[3]){ // west 
          addLeftWall();
          foundWalls = true; 
        }

        break;
      case 's':
        if(realMazeWalls[1]){ // south 
          addFrontWall();
          foundWalls = true; 
        }
        if(realMazeWalls[3]){ // west
          addRightWall();
          foundWalls = true; 
        }
        if(realMazeWalls[2]){ // east 
          addLeftWall();
          foundWalls = true; 
        }
        break; 
      case 'e':
        if(realMazeWalls[2]){ // east
          addFrontWall();
          foundWalls = true; 
        }
        if(realMazeWalls[1]){ // south 
          addRightWall();
          foundWalls = true; 
        }
        if(realMazeWalls[0]){// north 
          addLeftWall();
          foundWalls = true; 
        }
        break;
      case 'w': 
        if(realMazeWalls[3]){ // west 
          addFrontWall();
          foundWalls = true; 
        }
        if(realMazeWalls[0]){ // north 
          addRightWall();
          foundWalls = true; 
        }
        if(realMazeWalls[1]){ // south 
          addLeftWall();
          foundWalls = true; 
        }
        break; 

    }
    return foundWalls;
  }

  public void updateValues(){
    Stack<Cell> stack  = new Stack<Cell>(); 
    // push current cell 
    stack.Push(mmMaze.getCell(currentX,currentY));
    while(stack.Count > 0){
      // for top element of stack 
      Cell checkCell = stack.Pop();

      // get expected value -> smallestneighbor+1
      Cell smallestCell = chooseSmallestNeighbor(checkCell.getXCoord(),checkCell.getYCoord()).Item2; 
      int expectedValue = smallestCell.getValue() +1; 

      // validate actualUpdateCell value 
      if(checkCell.getValue() != expectedValue && checkCell.getValue() != 0){
        checkCell.setValue(expectedValue);
        List<Cell> neighbors = getOpenNeighbors(checkCell.getXCoord(),checkCell.getYCoord());
        for(int i = 0; i < neighbors.Count; i++){
          stack.Push(neighbors[i]);
        }
      }

      

    }

  }

  public char getNextMove(){
    // from neighbors (no walls between them) choose the smallest one 
    char newDir = chooseSmallestNeighbor(currentX, currentY).Item1;
    return newDir; 
  }

  public void turn(char newDir){
    currentDir = newDir; 
  }

  public void move(){
    switch(currentDir){
      case 'n':
        currentX -= 1; 
        break;
      case 's':
        currentX += 1;
        break; 
      case 'e':
        currentY += 1;
        break;
      case 'w': 
        currentY -= 1; 
        break; 
    }
  }


  public void addFrontWall(){
    // add current and neighbor cell if not exist 
    switch(currentDir){
      case 'n':
        addWallNorthNeighbor();
        break; 
      case 's':
        addWallSouthNeighbor();
        break; 
      case 'e':
        addWallEastNeighbor(); 
        break; 
      case 'w':
        addWallWestNeighbor(); 
        break; 
    }
  }

  public void addLeftWall(){
    switch(currentDir){
      case 'n':
        addWallWestNeighbor(); 
        break; 
      case 's':
        addWallEastNeighbor(); 
        break; 
      case 'e':
        addWallNorthNeighbor();
        break; 
      case 'w':
        addWallSouthNeighbor();
        break; 
    }

  }

  public void addRightWall(){
    switch(currentDir){
      case 'n':
        addWallEastNeighbor(); 
        break; 
      case 's':
        addWallWestNeighbor(); 
        break; 
      case 'e':
        addWallSouthNeighbor();
        break; 
      case 'w':
        addWallNorthNeighbor();
        break; 
    }

  }

  public void addWallNorthNeighbor(){
    if(!mmMaze.getCell(currentX,currentY).getNorthWall()){
        mmMaze.getCell(currentX,currentY).setNorthWall(true); 
        mmMaze.getCell(currentX-1,currentY).setSouthWall(true);
    }

  }
  public void addWallSouthNeighbor(){
    if(!mmMaze.getCell(currentX,currentY).getSouthWall()){
      mmMaze.getCell(currentX,currentY).setSouthWall(true); 
      mmMaze.getCell(currentX+1,currentY).setNorthWall(true);
    }

  }
  public void addWallEastNeighbor(){
    if(!mmMaze.getCell(currentX,currentY).getEastWall()){
      mmMaze.getCell(currentX,currentY).setEastWall(true); 
      mmMaze.getCell(currentX,currentY+1).setWestWall(true);
    }
  }
  public void addWallWestNeighbor(){
    if(!mmMaze.getCell(currentX,currentY).getWestWall()){
      mmMaze.getCell(currentX,currentY).setWestWall(true); 
      mmMaze.getCell(currentX,currentY-1).setEastWall(true);
    }
  }

  public Tuple<char,Cell> chooseSmallestNeighbor(int currentX, int currentY){
    char smallest = currentDir;  // variable to store new direction 
    Cell smallestCell = mmMaze.getCell(currentX,currentY);
    int min = 1000; // variable to compare min val 
    // N, S , E, W 
    bool[] walls = mmMaze.getCell(currentX,currentY).getWalls();  // get current cell walls 

    // If current dir has no walls set it min 
    switch(currentDir){
      case 'n':
        if(!walls[0]){
          min = mmMaze.getCell(currentX-1,currentY).getValue();
          smallestCell = mmMaze.getCell(currentX-1,currentY); 
        }
        break;
      case 's':
        if(!walls[1]){
          min = mmMaze.getCell(currentX+1,currentY).getValue();
          smallestCell = mmMaze.getCell(currentX+1,currentY); 
        }
        break; 
      case 'e':
        if(!walls[2]){
          min = mmMaze.getCell(currentX,currentY+1).getValue();
          smallestCell = mmMaze.getCell(currentX,currentY+1); 
        }
        break;
      case 'w': 
        if(!walls[3]){
         min = mmMaze.getCell(currentX,currentY-1).getValue();
         smallestCell = mmMaze.getCell(currentX,currentY-1); 
        }
        break; 
    }


    // Compare otherneighbors values 
    // compare north
    if(!walls[0] && min > mmMaze.getCell(currentX-1,currentY).getValue() ){
      smallest = 'n'; 
      smallestCell = mmMaze.getCell(currentX-1,currentY); 
      min = mmMaze.getCell(currentX-1,currentY).getValue(); 
    }
    // compare south 
    if(!walls[1] && min > mmMaze.getCell(currentX+1,currentY).getValue() ){
      smallest = 's'; 
      smallestCell = mmMaze.getCell(currentX+1,currentY); 
      min = mmMaze.getCell(currentX+1,currentY).getValue(); 
    } 
    // compare east 
    if(!walls[2] && min > mmMaze.getCell(currentX,currentY+1).getValue() ){
      smallest = 'e'; 
      smallestCell = mmMaze.getCell(currentX,currentY+1); 
      min = mmMaze.getCell(currentX,currentY+1).getValue();
    }
    // compare west
    if(!walls[3] && min > mmMaze.getCell(currentX,currentY-1).getValue() ){
      smallest = 'w'; 
      smallestCell = mmMaze.getCell(currentX,currentY-1); 
      min = mmMaze.getCell(currentX,currentY-1).getValue(); 
    }

    Tuple<char,Cell> res = new Tuple<char,Cell>(smallest,smallestCell);
    return res; 
  }

  

  public List<Cell> getAdjacent(int currentX, int currentY){
    List<Cell> neighbors = new List<Cell>(); 
    // North 
    if(currentX-1 >0){
      neighbors.Add(mmMaze.getCell(currentX-1,currentY));
    }
    // East 
    if(currentY+1 < mazeSize){
      neighbors.Add(mmMaze.getCell(currentX,currentY+1));
    }
    // West
    if(currentY-1 >0){
      neighbors.Add(mmMaze.getCell(currentX,currentY-1));
    }

    // South
    if(currentX+1 < mazeSize){
      neighbors.Add(mmMaze.getCell(currentX+1,currentY));
    }
    return neighbors; 

  }

    public List<Cell> getOpenNeighbors(int currentX, int currentY){
    List<Cell> neighbors = new List<Cell>(); 
    // North 
    if(currentX-1 >0){
      neighbors.Add(mmMaze.getCell(currentX-1,currentY));
    }
    // East 
    if(currentY+1 < mazeSize){
      neighbors.Add(mmMaze.getCell(currentX,currentY+1));
    }
    // West
    if(currentY-1 >0){
      neighbors.Add(mmMaze.getCell(currentX,currentY-1));
    }

    // South
    if(currentX+1 < mazeSize){
      neighbors.Add(mmMaze.getCell(currentX+1,currentY));
    }
    return neighbors; 

  }



  

}