def task7_stupid(fname):
    with open(fname, 'r') as fin:
        #mod = 998244353
        n, k = map(int, fin.readline().split())

        nums = list(map(int, fin.readline().split()))

        for p in range(1, k + 1):
            sum_result = 0

            for i in range(n - 1):
                for j in range(i + 1, n):
                    cur = (nums[i] + nums[j])
                    cur = cur ** p
                    sum_result = (sum_result + cur)

            print(sum_result)

def task7_smart(fname):
    with open(fname, 'r') as fin:
        #mod = 998244353
        n, k = map(int, fin.readline().split())

        nums = list(map(int, fin.readline().split()))
        nums_pows = [[0] * (k + 1) for _ in range(n)]
        for i in range(n):
            nums_pows[i][1] = nums[i]
            nums_pows[i][0] = 1

        for i in range(n):
            for p in range(2, k + 1):
                nums_pows[i][p] = (nums_pows[i][p - 1] * nums_pows[i][1])

        sums = [0] * (k + 1)
        for p in range(k + 1):
            cur = 0
            for i in range(n):
                cur = (cur + nums_pows[i][p])
            sums[p] = cur

        C = [[0] * (k + 1) for _ in range(k + 1)]
        C[0][0] = 1

        for i in range(1, k + 1):
            C[i][0] = 1
            C[i][i] = 1

        for i in range(2, k + 1):
            for j in range(1, i):
                C[i][j] = (C[i - 1][j - 1] + C[i - 1][j])

        for k_cur in range(1, k + 1):
            sum_result = 0

            for i in range(n):
                for p in range(k_cur + 1):
                    cur = C[k_cur][p]
                    cur = (cur * nums_pows[i][k_cur - p])
                    cur = (cur * (sums[p] - nums_pows[i][p]))
                    sum_result = (sum_result + cur)

            print((sum_result // 2))


task7_stupid("task7.txt")
print()
task7_smart("task7.txt")