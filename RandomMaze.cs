using System;
using System.Collections.Generic;

class RandomMaze{
  private Maze randMaze;
  private int missingCells; 

  public RandomMaze(int mazeSize,int startX,int startY){
    Console.WriteLine("Random Maze");
    missingCells = mazeSize*mazeSize;
    createRandomMaze(mazeSize, startX,startY); 
    randMaze.printMaze();
  }


  public Maze getMaze(){
    return randMaze;
  }


  public void createRandomMaze(int mazeSize,int startX, int startY){
    // create Maze with default values 
    randMaze = new Maze(mazeSize);

    // set initial wall configuration for maze 
    initialWalls(startX,startY); 
    randMaze.printMaze();

    Stack<Cell> stack = new Stack<Cell>();
    // Make the initial Cell current cell and mark it as visited 
    //mazeSize -2 as it is one after origin 
    Cell currentCell = new Cell(1,1,1);
    if(startX == 0){
      currentCell = randMaze.getCell(startX+1,startY);
    }else{
      currentCell = randMaze.getCell(startX-1,startY);
    }
    
    currentCell.setVisited(); 
    missingCells--; 

    // while there are unvisited cells 
    while(missingCells > 0){
      List<Cell> neighbors = getNeighbors(currentCell,mazeSize); 
      // if the current cell has not visited neighbors
      if(neighbors.Count > 0 ){
        // choose randomly one 
        Cell randomNeighbor = chooseRandomNeighbor(neighbors);

        // push current cell to the stack 
        stack.Push(currentCell);

        // delete  wall between current cell and chosen 
        deleteWalls(currentCell,randomNeighbor);

        // make the chosen cell the current and mark it as visited
        currentCell = randomNeighbor;
        currentCell.setVisited();
        missingCells--; 

      }else if(stack.Count > 0){ // else if the stack is not empty 
        // pop a cell from the stack 
        // make it the current cell 
        currentCell = stack.Pop();
      }
      
    }
      
  }

  private List<Cell> getNeighbors(Cell currentCell, int mazeSize){
    List<Cell> neighbors = new List<Cell>();
      int currentX = currentCell.getXCoord(); 
      int currentY = currentCell.getYCoord(); 
      //si no están visitados agregar a lista 
      // north  
      if(currentX-1 >= 0 && !randMaze.getCell(currentX-1,currentY).getVisited()){
        neighbors.Add(randMaze.getCell(currentX-1,currentY)); 
      } 
      // south 
      if(currentX+1 < mazeSize && !randMaze.getCell(currentX+1,currentY).getVisited()){
        neighbors.Add(randMaze.getCell(currentX+1,currentY)); 
      } 
      // east 
      if(currentY+1 < mazeSize && !randMaze.getCell(currentX,currentY+1).getVisited()){
        neighbors.Add(randMaze.getCell(currentX,currentY+1)); 
      } 
      // west
      if(currentY-1 >= 0 && !randMaze.getCell(currentX,currentY-1).getVisited()){
        neighbors.Add(randMaze.getCell(currentX,currentY-1)); 
      }

      return neighbors;  
  }

  public Cell chooseRandomNeighbor(List<Cell> neighbors){
    Random random = new Random();
    int randomNumber = random.Next(0, neighbors.Count);
    return neighbors[randomNumber];

  }

  public void deleteWalls(Cell current, Cell neighbor){
    if(current.getXCoord() == neighbor.getXCoord()){
        if(current.getYCoord() < neighbor.getYCoord() ){
          // East neighbor 
            current.setEastWall(false); 
            neighbor.setWestWall(false); 
            //Console.WriteLine(" ambos deben ser false: "+current.getEastWall()+" "+neighbor.getWestWall() );
        }else{
          // West neigbor 
            current.setWestWall(false); 
            neighbor.setEastWall(false); 
        }
    }else{
      if(current.getXCoord() > neighbor.getXCoord() ){
        // North neighbor 
          current.setNorthWall(false); 
          neighbor.setSouthWall(false); 
      }else{
        // South neigbor 
          current.setSouthWall(false); 
          neighbor.setNorthWall(false); 
      }
    }
  }

  public void initialWalls(int startX, int startY){
    // add all walls to maze 
    randMaze.setAllWalls();
    
    // set initial wall starting point 
    Cell currentCell = randMaze.getCell(startX,startY);
    if(startX == 0){
      deleteWalls(currentCell, randMaze.getCell(startX+1,startY));
    }else{
      deleteWalls(currentCell, randMaze.getCell(startX-1,startY));
    }
    currentCell.setVisited();
    missingCells--;
    
    //set center 
    // 0 : NW , 1: NE, 2: SE, 3:SW
    int mazeSize = randMaze.getMazeSize();
    int centerX = mazeSize/2; 
    int centerY = mazeSize/2; 
    Cell[] center = new Cell[4];
    bool[] walls  = new bool[8]; 
    // choose random wall to be entrance 
    Random random = new Random();
    int randomNumber = random.Next(0, 8);
    
    walls[randomNumber] = true; 

    // get center cells for manipulation 
    center[0] = randMaze.getCell(centerX,centerY);
    center[1] = randMaze.getCell(centerX-1,centerY);
    center[2] = randMaze.getCell(centerX-1,centerY-1); 
    center[3] = randMaze.getCell(centerX,centerY-1); 

    // Set center cells as visited 
    center[0].setVisited();
    missingCells--;
    center[1].setVisited(); 
    missingCells--;
    center[2].setVisited();
    missingCells--;
    center[3].setVisited();
    missingCells--;

    // South East cell 
    // delete center walls
    center[0].setWestWall(false);
    center[0].setNorthWall(false);
    // entrance if true will be open 
    center[0].setSouthWall(!walls[0]);
    center[0].setEastWall(!walls[1]);
      // update neighbors 
      randMaze.getCell(centerX+1,centerY).setNorthWall(!walls[0]);
      randMaze.getCell(centerX,centerY+1).setWestWall(!walls[1]);
    

    // North East Cell 
    center[1].setSouthWall(false);
    center[1].setWestWall(false);
    //entrance 
    center[1].setNorthWall(!walls[2]);
    center[1].setEastWall(!walls[3]);
      // update neighbors 
      randMaze.getCell(centerX-2,centerY).setSouthWall(!walls[2]);
      randMaze.getCell(centerX-1,centerY+1).setWestWall(!walls[3]);
    

    // North West Cell 
    
    center[2].setSouthWall(false);
    center[2].setEastWall(false);
    //entrance 
    center[2].setNorthWall(!walls[4]);
    center[2].setWestWall(!walls[5]);
      // set neighbors 
      randMaze.getCell(centerX-2,centerY-1).setSouthWall(!walls[4]);
      randMaze.getCell(centerX-1,centerY-2).setEastWall(!walls[5]);
    

    // South West Cell 
    
    center[3].setNorthWall(false);
    center[3].setEastWall(false);
    //  entrance
    center[3].setSouthWall(!walls[6]);
    center[3].setWestWall(!walls[7]);
      // set neighbors 
      randMaze.getCell(centerX+1,centerY-1).setNorthWall(!walls[6]);
      randMaze.getCell(centerX,centerY-2).setEastWall(!walls[7]); 
    
  }
}