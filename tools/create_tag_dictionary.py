import re

FILENAME = 'tags.txt'

def main():
    pattern = '\((.{4}),(.{4})\)\t(.+)\t(.+)\t'
    with open(FILENAME) as f:
        for line in f:
            # print(line.strip())
            result = re.findall(pattern, line)
            if result:
                print(str.format('{{ new Tag(0x{}, 0x{}), new TagInfo(\"{}\", \"{}\") }},', result[0][0], result[0][1], result[0][2], result[0][3]))


if __name__ == "__main__":
    main()