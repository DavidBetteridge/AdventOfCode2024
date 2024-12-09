namespace AdventOfCode2024.Solutions;

public class Day09
{

    private const int FreeSpace = int.MaxValue;
    private sealed record Block(int FileId, int Length, int OriginalPos)
    {
        public int FileId { get; set; } = FileId;
        public int Length { get; set; } = Length;
    }

    public long Part1(string filename)
    {
        var input = File.ReadAllBytes(filename);
        var i = 0;
        var freeSpaceBlockCount = 0;
        var nextFileId = 0;
        var blocks = new LinkedList<Block>();

        // Parse file
        while (i < input.Length)
        {
            if (i % 2 == 0)
            {
                blocks.AddLast(new Block(nextFileId, input[i] - '0', i));
                nextFileId++;
            }
            else
            {
                blocks.AddLast(new Block(FreeSpace, input[i] - '0', i));
                freeSpaceBlockCount++;
            }

            i++;
        }

        // Find the first 
        var nextFree = blocks.First!.Next!;

        // Find the last file
        var fileToExamine = blocks.Last;
        if (fileToExamine!.Value.FileId == FreeSpace)
        {
            // The last entry was freespace
            freeSpaceBlockCount--;
            fileToExamine = fileToExamine.Previous;
        }

        while (freeSpaceBlockCount > 0)
        {
            // We want to move the file final into the first free space.

            // Case 1.  File is equal to the free space.
            if (fileToExamine!.Value.Length == nextFree!.Value.Length)
            {
                // Swap the file with the free space
                nextFree.Value.FileId = fileToExamine!.Value.FileId;
                fileToExamine!.Value.FileId = FreeSpace;
                freeSpaceBlockCount--;
                
                // Find the next free block
                var next = nextFree.Next;
                nextFree = next;
                while (nextFree is not null && nextFree!.Value.FileId != FreeSpace)
                    nextFree = nextFree.Next;

                blocks.Remove(fileToExamine);
                fileToExamine = blocks.Last;
                if (fileToExamine!.Value.FileId == FreeSpace)
                {
                    // The last entry was freespace
                    freeSpaceBlockCount--;
                    blocks.RemoveLast();
                    fileToExamine = blocks.Last;
                }
            }


            // Case 2.  File is smaller than the free space
            else if (fileToExamine!.Value.Length < nextFree!.Value.Length)
            {
                blocks.AddBefore(
                    nextFree,
                    new LinkedListNode<Block>(fileToExamine!.Value)
                );

                //Renaming space
                blocks.AddBefore(
                    nextFree,
                    new LinkedListNode<Block>(
                        new Block(FreeSpace, nextFree!.Value.Length - fileToExamine!.Value.Length, i))
                );
                var next = nextFree.Previous;
                blocks.Remove(nextFree);
                nextFree = next;

                blocks.Remove(fileToExamine);
                fileToExamine = blocks.Last;
                if (fileToExamine!.Value.FileId == FreeSpace)
                {
                    // The last entry was freespace
                    freeSpaceBlockCount--;
                    blocks.RemoveLast();
                    fileToExamine = blocks.Last;
                }
            }


            // Case 3.  File is larger than the free space.
            else if (fileToExamine!.Value.Length > nextFree!.Value.Length)
            {
                var spaceAvailable = nextFree!.Value.Length;
                // Insert as much as we can
                blocks.AddBefore(
                    nextFree,
                    new LinkedListNode<Block>(new Block(fileToExamine.Value.FileId, nextFree!.Value.Length, i))
                );

                var next = nextFree.Next;
                blocks.Remove(nextFree);
                freeSpaceBlockCount--;

                // Find the next free block
                nextFree = next;
                while (nextFree is not null && nextFree!.Value.FileId != FreeSpace)
                    nextFree = nextFree.Next;

                // Shorten the final block
                var removed = fileToExamine.Value;
                blocks.Remove(fileToExamine);
                blocks.AddLast(new Block(removed.FileId, removed.Length - spaceAvailable, i));
                fileToExamine = blocks.Last;
            }
        }

        // Walk list to create checksum
        var total = 0L;
        var position = 0;
        var block = blocks.First!;
        do
        {
            // Loop over the length
            for (var j = 0; j < block.Value.Length; j++)
            {
                total += block.Value.FileId * position;
                position++;
            }

            block = block.Next;
        } while (block is not null);

        return total;
    }
    
    private sealed record FreeSpaceBlock
    {
        public LinkedListNode<Block> Block { get; set; }
        public int OriginalPos { get; set; }
    }
    
    public long Part2(string filename)
    {
        var input = File.ReadAllBytes(filename);
        var i = 0;
        var nextFileId = 0;
        var blocks = new LinkedList<Block>();
        
        var freespaceLists = new List<FreeSpaceBlock>[10];
        for (var j = 1; j < 10; j++)
            freespaceLists[j] = [];

        var freespaceListHeads = new int[10];
        
        // Parse file
        while (i < input.Length)
        {
            if (i % 2 == 0)
            {
                blocks.AddLast(new Block(nextFileId, input[i] - '0', i));
                nextFileId++;
            }
            else
            {

                if (input[i] - '0' > 0)
                {
                    var added = blocks.AddLast(new Block(FreeSpace, input[i] - '0', i));
                    freespaceLists[input[i] - '0'].Add(new FreeSpaceBlock
                    {
                        Block = added,
                        OriginalPos = i
                    });
                }
            }

            i++;
        }

        // Find the last file
        var fileToExamine = blocks.Last;
        if (fileToExamine!.Value.FileId == FreeSpace)
            fileToExamine = fileToExamine.Previous;
        
        while (fileToExamine is not null && fileToExamine.Value.FileId != FreeSpace)
        {
         //   Debug(blocks);
            
            FreeSpaceBlock? freeSpaceBlockPointer = null;
            for (var j = fileToExamine.Value.Length; j < 10; j++)
            {
                var headOfList = freespaceListHeads[j];
                if (headOfList < freespaceLists[j].Count)
                {
                    var entry = freespaceLists[j][headOfList];
                    if (entry.OriginalPos < fileToExamine.Value.OriginalPos)
                    {
                        if (freeSpaceBlockPointer is null || freeSpaceBlockPointer.OriginalPos > entry.OriginalPos)
                            freeSpaceBlockPointer = entry;
                    }
                }
            }
            
            if (freeSpaceBlockPointer is not null)
            {
                // We have a space where we can insert the file
                var freeSpace = freeSpaceBlockPointer.Block;
                freespaceListHeads[freeSpace.Value.Length] += 1;
                
                var remaining = freeSpace.Value.Length - fileToExamine.Value.Length;
                freeSpace.Value.FileId = fileToExamine.Value.FileId;
                if (remaining > 0)
                {
                    freeSpace.Value.Length = fileToExamine.Value.Length;
                    blocks.AddAfter(
                        freeSpace,
                        new LinkedListNode<Block>(
                            new Block(FreeSpace, remaining, freeSpace.Value.OriginalPos))
                    );

                    var k = 0;
                    while (k < freespaceLists[remaining].Count &&
                           freespaceLists[remaining][k].OriginalPos < freeSpace.Value.OriginalPos)
                        k++;
                    freespaceLists[remaining].Insert(k, new FreeSpaceBlock
                    {
                        Block = freeSpace.Next!,
                        OriginalPos = freeSpace.Value.OriginalPos
                    });
                }
                
                fileToExamine.Value.FileId = FreeSpace;
            }
            
            fileToExamine = fileToExamine.Previous;
            while (fileToExamine is not null && fileToExamine.Value.FileId == FreeSpace)
                fileToExamine = fileToExamine.Previous;
            
        }
        
        // Walk list to create checksum
        var total = 0L;
        var position = 0;
        var block = blocks.First!;
        do
        {
            // Loop over the length
            for (var j = 0; j < block.Value.Length; j++)
            {
                if (block.Value.FileId != FreeSpace)
                {
                    total += block.Value.FileId * position;
                }
                position++;
            }

            block = block.Next;
        } while (block is not null);

        return total;
    }

    
    private void Debug(LinkedList<Block> blocks)
    {
        Console.WriteLine();
        var block = blocks.First!;
        do
        {
            for (var j = 0; j < block.Value.Length; j++)
                Console.Write(block.Value.FileId == FreeSpace ? "." : block.Value.FileId);

            block = block.Next;
        } while (block is not null);

    }
}