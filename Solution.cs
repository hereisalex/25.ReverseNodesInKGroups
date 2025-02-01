public class Solution
{
    public class ListNode
    {
        public int val;
        public ListNode? next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    public static void Main(string[] args)
    {

        ListNode head = new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(5)))));
        int k = 2;
        Solution solution = new Solution();
        ListNode result = solution.ReverseKGroup(head, k);
    }

    public ListNode ReverseKGroup(ListNode head, int k) //this method expands on the train analogy from 24.SwapNodesInPairs
    {
        if (head == null || k == 1)
        {
            return head;
        }

        // Create a 'station' - a temporary holding area before the actual train
        ListNode station = new ListNode(0);
        station.next = head;

        // The 'trainMaster' oversees the entire reversal process from the station
        ListNode trainMaster = station;
        int count = 0;

        // The 'scout' checks ahead to see if there are enough carriages for a k-group reversal
        ListNode scout = head;

        while (scout != null)
        {
            count++;
            if (count % k == 0)
            {
                // Enough carriages for a reversal!
                // The trainMaster will manage the reversal of the k-group starting from trainMaster.next
                trainMaster = reverseKNodes(trainMaster, scout.next);
                scout = trainMaster.next;
            }
            else
            {
                scout = scout.next;
            }
        }
        return station.next;
    }

    // Helper function to reverse a k-group (like a section of the train)
    private ListNode reverseKNodes(ListNode start, ListNode end)
    {
        // Within a k-group:
        // 'start' is like the beginning of the section we want to reverse
        // 'end' is like the carriage just after the section we want to reverse

        // Initial state (within a k-group):
        //
        //   start           end
        //     |              |
        //   station -> [1] -> [2] -> [3] -> [4] -> ... -> [k] -> ...
        //             first                                 next
        //             carriage                              group

        ListNode previous = start; // The 'railway worker' setting up the tracks for reversal
        ListNode current = start.next; // The 'carriage' being reversed
        ListNode firstCarriage = current; // Keep track of the original first carriage, it will become the last of the reversed group

        while (current != end)
        {
            ListNode nextCarriage = current.next; // The next carriage to be processed
            // Reverse the connection:
            current.next = previous;

            //  Diagram of one step in the reversing process:
            //
            //                     previous current nextCarriage
            //                        |        |      |
            //  start -> [1] <- [2]    [3] -> [4] -> [5] ... -> [k] -> ...
            //

            // Move the railway worker and the current carriage forward
            previous = current;
            current = nextCarriage;
        }

        // After reversing the section:
        //
        //   start           end
        //     |              |
        //   station -> [k] -> ... -> [2] -> [1] -> [next group]
        //                                      ^
        //                                      |
        //                                 firstCarriage (now at the end)

        // Connect the reversed section back to the main train line:
        start.next = previous; // The trainMaster (start) now points to the new first carriage of the reversed group
        firstCarriage.next = current; // The original first carriage now points to the next group (end)

        // The trainMaster moves to the end of the reversed group (which was the original first carriage)
        return firstCarriage;
    }
}
