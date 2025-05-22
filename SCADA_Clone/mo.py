import sys
import math

def main():
    n = int(input())
    
    p = list(map(int, input().split()))
    
    v = list(map(int, input().split()))

    sum = 0

    for i in range(1, n + 1):
        for j in range(1, i):
            I1 = p[i - 1] == p[j - 1]
            I2 = v[i - 1] == v[j - 1]
            sum += I1 == I2

    P = sum * 2
    Q = n * (n - 1)
    d = math.gcd(P, Q)

    print(f"{P // d}/{Q // d}")

if __name__ == '__main__':
    main()