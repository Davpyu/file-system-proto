using Proto.App.Models;

namespace Proto.App.Services;

public class AvlTreeService
{
    public Metadata _root;

    public Metadata InsertData(Metadata parent, Metadata metadata)
    {
        if (parent == null)
        {
            return metadata;
        }

        if (metadata.Id < parent.Id)
        {
            parent.Left = InsertData(parent.Left, metadata);
        }

        else if (metadata.Id > parent.Id)
        {
            parent.Right = InsertData(parent.Right, metadata);
        }

        else
        {
            return parent;
        }

        parent.Height = 1 + int.Max(GetHeight(parent.Left), GetHeight(parent.Right));

        int balance = BalanceFactor(parent);

        if (balance > 1)
        {
            if (metadata.Id < parent.Left.Id)
            {
                return RightRotate(parent);
            }

            if(metadata.Id > parent.Right.Id)
            {
                return LeftRotate(parent);
            }

            if(metadata.Id > parent.Left.Id)
            {
                parent.Left = LeftRotate(parent.Left);
                return RightRotate(parent);
            }
        }

        if (balance < -1 && metadata.Id < parent.Right.Id)
        {
            parent.Right = RightRotate(parent.Right);
            return LeftRotate(parent);
        }

        return parent;
    }

    public Metadata DeleteData(Metadata metadata, int id)
    {
        if (metadata == null)
        {
            return metadata;
        }

        if (id < metadata.Id)
        {
            metadata.Left = DeleteData(metadata.Left, id);
        }

        else if (id > metadata.Id)
        {
            metadata.Right = DeleteData(metadata.Right, id);
        }

        else
        {
            if ((metadata.Left == null) || (metadata.Right == null))
            {
                Metadata temp = null;
                if (temp == metadata.Left)
                {
                    temp = metadata.Right;
                }
                else
                {
                    temp = metadata.Left;
                }

                if (temp == null)
                {
                    temp = metadata;
                    metadata = null;
                }
                else
                {
                    metadata = temp;
                }
            }
            else
            {
                Metadata temp = MinValueMetadata(metadata.Right);

                metadata.Id = temp.Id;
                metadata.Created = temp.Created;
                metadata.Modified = temp.Modified;
                metadata.Name = temp.Name;
                metadata.Size = temp.Size;

                metadata.Right = DeleteData(metadata.Right, temp.Id);
            }
        }

        if (metadata == null)
        {
            return metadata;
        }

        metadata.Height = int.Max(GetHeight(metadata.Left), GetHeight(metadata.Right)) + 1;

        int balance = BalanceFactor(metadata);

        if (balance > 1 && BalanceFactor(metadata.Left) >= 0)
        {
            return RightRotate(metadata);
        }

        if (balance > 1 && BalanceFactor(metadata.Left) < 0)
        {
            metadata.Left = LeftRotate(metadata.Left);
            return RightRotate(metadata);
        }

        if (balance < -1 && BalanceFactor(metadata.Right) <= 0)
        {
            return LeftRotate(metadata);
        }

        if (balance < -1 && BalanceFactor(metadata.Right) > 0)
        {
            metadata.Right = RightRotate(metadata.Right);
            return LeftRotate(metadata);
        }

        return metadata;
    }

    public static Metadata Search(Metadata metadata, int id)
    {
        if (metadata == null)
        {
            return metadata;
        }

        if (id < metadata.Id)
        {
            return Search(metadata.Left, id);
        }

        else if(id > metadata.Id)
        {
            return Search(metadata.Right, id);
        }

        return metadata;
    }

    public static void PreOrder(Metadata Metadata)
    {
        if (Metadata != null)
        {
            Console.WriteLine(Metadata.Serialize());
            PreOrder(Metadata.Left);
            PreOrder(Metadata.Right);
        }
    }

    private static int GetHeight(Metadata metadata)
    {
        if (metadata == null)
        {
            return 0;
        }

        return metadata.Height;
    }

    private Metadata RightRotate(Metadata metadata)
    {
        Metadata left = metadata.Left;
        Metadata right = left.Right;

        left.Right = metadata;
        metadata.Left = right;

        metadata.Height = int.Max(GetHeight(metadata.Left), GetHeight(metadata.Right)) + 1;
        left.Height = int.Max(GetHeight(left.Left), GetHeight(left.Right)) + 1;

        return left;
    }

    private Metadata LeftRotate(Metadata metadata)
    {
        Metadata right = metadata.Right;
        Metadata left = right.Left;

        right.Left = metadata;
        metadata.Right = left;

        metadata.Height = int.Max(GetHeight(metadata.Left), GetHeight(metadata.Right)) + 1;
        right.Height = int.Max(GetHeight(right.Left), GetHeight(right.Right)) + 1;

        return right;
    }

    private int BalanceFactor(Metadata metadata)
    {
        if (metadata == null)
        {
            return 0;
        }

        return GetHeight(metadata.Left) - GetHeight(metadata.Right);
    }

    private static Metadata MinValueMetadata(Metadata Metadata)
    {
        Metadata current = Metadata;

        while (current.Left != null)
        {
            current = current.Left;
        }

        return current;
    }

}