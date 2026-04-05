# StS2SeedRoller

杀戮尖塔 2 (Slay the Spire 2) 搜种器，自动搜索种子，找到你想要的农种

## 目前支持功能

- **给定种子，查询该种子的初始捏奥选项**
- **给定遗物，搜索初始捏奥选项给予该遗物的种子**


## 编译需求

需要 [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)。

```bash
cd StS2SeedRoller
dotnet build
```

## 使用方法

### 搜索种子

```bash
# 搜索铁甲战士获得 Kunai 的种子
dotnet run -- --relic Kunai --character Ironclad

# 搜索静默猎手获得 IceCream 的种子，找 3 个结果
dotnet run -- --relic IceCream --character Silent --count 3

# 指定线程数
dotnet run -- --relic Shuriken --character Defect --threads 8
```

### 检查种子

```bash
# 查看指定种子的捏奥选项
dotnet run -- --check 84B4ZYNNMK --character Ironclad

# 检查并匹配目标遗物
dotnet run -- --check AAAAAAAAAA --character Ironclad --relic MummifiedHand
```

### 参数说明

| 参数 | 缩写 | 说明 | 默认值 |
|------|------|------|--------|
| `--relic` | `-r` | 目标遗物英语名称 | 必填 (搜索模式) |
| `--character` | `-c` | 角色名称 | Ironclad |
| `--threads` | `-t` | 搜索线程数 | CPU 核心数 |
| `--count` | `-n` | 目标结果数 | 1 |
| `--check` | - | 检查指定种子 | - |

### 角色名称

| 英文 | 中文 |
|------|------|
| Ironclad | 铁甲战士 |
| Silent | 静默猎手 |
| Defect | 机器人 |
| Necrobinder | 亡灵法师 |
| Regent | 储君 |

## 输出示例

```
========================================
  Seed: 84B4ZYNNMK
  Character: Ironclad
  Neow Options:
    - NewLeaf (positive)
    - PreciseScissors (positive)
    - LargeCapsule (curse) <--
  Capsule: LargeCapsule (2 relics + Strike + Defend)
  Obtained relics: GamePiece, LetterOpener
========================================
```

## 注意事项

- 遗物名称使用英文类名（如 `Kunai`、`IceCream`），大小写不敏感
- 假设所有遗物已解锁
- 仅支持单人模式
