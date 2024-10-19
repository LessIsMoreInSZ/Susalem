interface DiskInfo {
  diskName: string;
  typeName: string;
  totalFree: number;
  totalSize: number;
  used: number;
  availableFreeSpace: number;
  availablePercent: number;
}

interface MemoryInfo {
  total: number;
  used: number;
  free: number;
  usedRam: string;
  cpuRate: string;
  totalRAM: string;
  ramRate: string;
  freeRam: string;
  appRAMRate: string;
}

interface SystemInfo {
  machineName: string;
  osName: string;
  osArchitecture: string;
  doNetName: string;
  ip: string;
  cpuCount: number;
  useRam: string;
  startTime: string;
  runTime: string;
  diskInfo: DiskInfo[];
  memoryInfo: MemoryInfo;
}
