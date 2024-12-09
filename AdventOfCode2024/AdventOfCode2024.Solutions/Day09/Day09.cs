namespace AdventOfCode2024.Solutions;

public class Day09
{

    private const int FreeSpace = int.MaxValue;
    private sealed record Block(int FileId, int Length);

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
                blocks.AddLast(new Block(nextFileId, input[i] - '0'));
                nextFileId++;
            }
            else
            {
                blocks.AddLast(new Block(FreeSpace, input[i] - '0'));
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
            blocks.RemoveLast();
            fileToExamine = blocks.Last;
        }

        while (freeSpaceBlockCount > 0)
        {
            // We want to move the file final into the first free space.

            // Case 1.  File is equal to the free space.
            if (fileToExamine!.Value.Length == nextFree!.Value.Length)
            {
                blocks.AddBefore(
                    nextFree,
                    new LinkedListNode<Block>(fileToExamine!.Value)
                );
                var next = nextFree.Next;
                blocks.Remove(nextFree);
                freeSpaceBlockCount--;

                // Find the next free block
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
                        new Block(FreeSpace, nextFree!.Value.Length - fileToExamine!.Value.Length))
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
                    new LinkedListNode<Block>(new Block(fileToExamine.Value.FileId, nextFree!.Value.Length))
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
                blocks.AddLast(new Block(removed.FileId, removed.Length - spaceAvailable));
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
    
    
      public long Part2(string filename)
    {
        var input = File.ReadAllBytes(filename);
        var i = 0;
        var nextFileId = 0;
        var blocks = new LinkedList<Block>();

        // Parse file
        while (i < input.Length)
        {
            if (i % 2 == 0)
            {
                blocks.AddLast(new Block(nextFileId, input[i] - '0'));
                nextFileId++;
            }
            else
            {
                blocks.AddLast(new Block(FreeSpace, input[i] - '0'));
            }

            i++;
        }

        // Find the last file
        var fileToExamine = blocks.Last;
        if (fileToExamine!.Value.FileId == FreeSpace)
        {
            // The last entry was freespace
            blocks.RemoveLast();
            fileToExamine = blocks.Last;
        }

        while (fileToExamine is not null && fileToExamine.Value.FileId != FreeSpace)
        {
            var freeSpace = blocks.First;
            while (freeSpace is not null && (freeSpace.Value.FileId != FreeSpace ||
                                             freeSpace.Value.Length < fileToExamine.Value.Length))
            {
                if (freeSpace!.Value.FileId == fileToExamine.Value.FileId)
                    freeSpace = null;
                else
                    freeSpace = freeSpace.Next;
            }

            if (freeSpace is not null &&
                freeSpace.Value.FileId == FreeSpace &&
                freeSpace.Value.Length >= fileToExamine.Value.Length)
            {
                // We have a space where we can insert the file
                
                // Insert the file
                blocks.AddBefore(
                    freeSpace,
                    new LinkedListNode<Block>(fileToExamine!.Value)
                );

                //Renaming space
                var remaining = freeSpace!.Value.Length - fileToExamine!.Value.Length;
                if (remaining > 0)
                {
                    blocks.AddBefore(
                        freeSpace,
                        new LinkedListNode<Block>(
                            new Block(FreeSpace, remaining))
                    );
                }

                // Remove the free space
                blocks.Remove(freeSpace);
                
                // Remove the file from the end of the list
                var tmp = fileToExamine.Previous;
                
                blocks.AddAfter(
                    fileToExamine,
                    new LinkedListNode<Block>(
                        new Block(FreeSpace, fileToExamine!.Value.Length))
                );
                
                blocks.Remove(fileToExamine);
                while (tmp is not null && tmp.Value.FileId == FreeSpace)
                    tmp = tmp.Previous;
                fileToExamine = tmp;
            }
            else
            {
                // No space for this file
                fileToExamine = fileToExamine.Previous;
                while (fileToExamine is not null && fileToExamine.Value.FileId == FreeSpace)
                    fileToExamine = fileToExamine.Previous;
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
}