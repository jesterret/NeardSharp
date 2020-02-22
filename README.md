# NeardSharp

Port of libneardal to .NET Standard.  
It does not take dependencies on the library, but rather connects to daemon itself over DBus.

DBus support was achieved with help of [Tmds.DBus library](https://github.com/tmds/Tmds.DBus)

### Remarks

Used Neard daemon version is not the latest one, as NXP ExploreNFC board has it's own implementation on top of older version, making otherwise incompatible board work with it.  
Should still work fine with older version, albeit with less data available in some cases.