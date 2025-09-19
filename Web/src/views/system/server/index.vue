<template>
	<div class="device-info-container">
		<!-- 系统概览卡片 -->
		<el-row :gutter="16" style="margin-bottom: 16px">
			<el-col :span="24">
				<el-card class="overview-card" shadow="hover" v-loading="systemInfoLoading" element-loading-text="正在获取系统信息...">
					<template #header>
						<div class="card-header">
							<i class="el-icon-monitor"></i>
							<span>系统概览</span>
							<span class="status-text" v-if="!systemInfoLoading">运行正常</span>
						</div>
					</template>
					<div class="overview-content">
						<div class="overview-item">
							<div class="overview-icon cpu-icon">
								<i class="el-icon-cpu"></i>
							</div>
							<div class="overview-text">
								<div class="overview-title">处理器</div>
								<div class="overview-value">{{ deviceInfo.cpuInfo.processorName }}</div>
								<div class="overview-subtitle">{{ deviceInfo.cpuInfo.physicalCoreCount }}核 {{ deviceInfo.cpuInfo.logicalCoreCount }}线程</div>
							</div>
						</div>
						<div class="overview-item">
							<div class="overview-icon ram-icon">
								<i class="el-icon-film"></i>
							</div>
							<div class="overview-text">
								<div class="overview-title">内存</div>
								<div class="overview-value">{{ deviceInfo.ramInfo.totalSpace }}</div>
								<div class="overview-subtitle">使用率 {{ deviceInfo.ramInfo.usagePercentage }}%</div>
							</div>
						</div>
						<div class="overview-item">
							<div class="overview-icon os-icon">
								<i class="el-icon-monitor"></i>
							</div>
							<div class="overview-text">
								<div class="overview-title">操作系统</div>
								<div class="overview-value">{{ deviceInfo.systemInfo.osDescription }}</div>
								<div class="overview-subtitle">{{ deviceInfo.systemInfo.osArchitecture }} 架构</div>
							</div>
						</div>
						<div class="overview-item">
							<div class="overview-icon framework-icon">
								<i class="el-icon-guide"></i>
							</div>
							<div class="overview-text">
								<div class="overview-title">运行框架</div>
								<div class="overview-value">{{ deviceInfo.systemInfo.frameworkDescription }}</div>
								<div class="overview-subtitle">{{ deviceInfo.systemInfo.machineName }}</div>
							</div>
						</div>
						<div class="overview-item">
							<div class="overview-icon time-icon">
								<i class="el-icon-time"></i>
							</div>
							<div class="overview-text">
								<div class="overview-title">运行时间</div>
								<div class="overview-value">{{ deviceInfo.systemUptime }}</div>
								<div class="overview-subtitle">系统稳定运行</div>
							</div>
						</div>
					</div>
				</el-card>
			</el-col>
		</el-row>

		<!-- CPU和内存性能 -->
		<el-row :gutter="16" style="margin-bottom: 16px">
			<el-col :md="12" :sm="24">
				<el-card class="performance-card cpu-card" shadow="hover" v-loading="systemInfoLoading">
					<template #header>
						<div class="card-header">
							<i class="el-icon-cpu"></i>
							<span>CPU 性能</span>
						</div>
					</template>
					<div class="performance-content">
						<div class="performance-chart">
							<el-progress type="dashboard" :percentage="deviceInfo.cpuInfo.usagePercentage" :color="getCpuColor(deviceInfo.cpuInfo.usagePercentage)" :width="180" :stroke-width="12">
								<template #default>
									<div class="progress-content">
										<div class="progress-value">{{ deviceInfo.cpuInfo.usagePercentage }}%</div>
										<div class="progress-label">CPU使用率</div>
									</div>
								</template>
							</el-progress>
						</div>
						<div class="performance-details">
							<div class="detail-item">
								<span class="detail-label">处理器架构：</span>
								<span class="detail-value">{{ deviceInfo.cpuInfo.processorArchitecture }}</span>
							</div>
							<div class="detail-item">
								<span class="detail-label">基础频率：</span>
								<span class="detail-value">{{ deviceInfo.cpuInfo.baseClockSpeed }} GHz</span>
							</div>
							<div class="detail-item">
								<span class="detail-label">缓存大小：</span>
								<span class="detail-value">{{ deviceInfo.cpuInfo.cacheSize }}</span>
							</div>
						</div>
					</div>
				</el-card>
			</el-col>
			<el-col :md="12" :sm="24">
				<el-card class="performance-card ram-card" shadow="hover" v-loading="systemInfoLoading">
					<template #header>
						<div class="card-header">
							<i class="el-icon-memory-card"></i>
							<span>内存使用</span>
						</div>
					</template>
					<div class="performance-content">
						<div class="performance-chart">
							<el-progress type="dashboard" :percentage="deviceInfo.ramInfo.usagePercentage" :color="getRamColor(deviceInfo.ramInfo.usagePercentage)" :width="180" :stroke-width="12">
								<template #default>
									<div class="progress-content">
										<div class="progress-value">{{ deviceInfo.ramInfo.usagePercentage.toFixed(1) }}%</div>
										<div class="progress-label">内存使用</div>
									</div>
								</template>
							</el-progress>
						</div>
						<div class="performance-details">
							<div class="detail-item">
								<span class="detail-label">总内存：</span>
								<span class="detail-value">{{ deviceInfo.ramInfo.totalSpace }}</span>
							</div>
							<div class="detail-item">
								<span class="detail-label">已使用：</span>
								<span class="detail-value">{{ deviceInfo.ramInfo.usedSpace }}</span>
							</div>
							<div class="detail-item">
								<span class="detail-label">可用：</span>
								<span class="detail-value">{{ deviceInfo.ramInfo.freeSpace }}</span>
							</div>
						</div>
					</div>
				</el-card>
			</el-col>
		</el-row>

		<!-- 磁盘信息 -->
		<el-row :gutter="16" style="margin-bottom: 16px">
			<el-col :span="24">
				<el-card class="disk-card" shadow="hover" v-loading="systemInfoLoading">
					<template #header>
						<div class="card-header">
							<i class="el-icon-files"></i>
							<span>磁盘信息</span>
							<span class="status-text">{{ deviceInfo.diskInfos.length }} 个磁盘</span>
						</div>
					</template>
					<el-row :gutter="16">
						<el-col :md="12" :sm="24" v-for="disk in deviceInfo.diskInfos" :key="disk.diskName">
							<div class="disk-item">
								<div class="disk-content">
									<div class="disk-chart">
										<el-progress type="circle" :percentage="parseFloat(disk.usedPercentage)" :color="getDiskColor(disk.availableRate)" :width="100" :stroke-width="8">
											<template #default>
												<div class="disk-progress-content">
													<div class="disk-usage">{{ disk.usedPercentage }}%</div>
												</div>
											</template>
										</el-progress>
									</div>
									<div class="disk-all-info">
										<div class="disk-title-section">
											<span class="disk-name">{{ disk.diskName }}</span>
											<span class="disk-type">{{ disk.typeName }}</span>
										</div>
										<div class="disk-capacity-info">
											<div class="capacity-item">
												<span class="capacity-label">总容量</span>
												<span class="capacity-value">{{ disk.totalSpace }}</span>
											</div>
											<div class="capacity-item">
												<span class="capacity-label">已使用</span>
												<span class="capacity-value">{{ disk.usedSpace }}</span>
											</div>
											<div class="capacity-item">
												<span class="capacity-label">可用空间</span>
												<span class="capacity-value">{{ disk.freeSpace }}</span>
											</div>
										</div>
									</div>
								</div>
							</div>
						</el-col>
					</el-row>
				</el-card>
			</el-col>
		</el-row>

		<!-- GPU信息 -->
		<el-row :gutter="16" style="margin-bottom: 16px">
			<el-col :span="24">
				<el-card class="gpu-card" shadow="hover" v-loading="systemInfoLoading">
					<template #header>
						<div class="card-header">
							<i class="el-icon-view"></i>
							<span>显卡信息</span>
							<span class="status-text">{{ deviceInfo.gpuInfos.length }} 个显卡</span>
						</div>
					</template>
					<div class="gpu-content">
						<el-collapse v-model="activeGpuNames" accordion>
							<el-collapse-item v-for="(gpu, index) in deviceInfo.gpuInfos" :key="index" :name="gpu.name">
								<template #title>
									<div class="gpu-title">
										<div class="gpu-status">
											<div class="gpu-type-icon">
												<i class="el-icon-monitor"></i>
											</div>
											<div class="gpu-info-summary">
												<div class="gpu-name">{{ gpu.name }}</div>
												<div class="gpu-memory">{{ gpu.memorySize }}</div>
											</div>
										</div>
										<div class="gpu-badges">
											<el-tag :type="gpu.status === 'OK' ? 'success' : 'danger'" size="small" class="status-tag">
												{{ gpu.status }}
											</el-tag>
											<div class="driver-info">
												<span class="driver-text">驱动 {{ gpu.driverVersion || 'N/A' }}</span>
											</div>
										</div>
									</div>
								</template>
								<div class="gpu-details">
									<div class="gpu-specs-grid">
										<div class="spec-card">
											<div class="spec-card-icon memory-icon">
												<i class="el-icon-cpu"></i>
											</div>
											<div class="spec-card-content">
												<div class="spec-card-label">显存</div>
												<div class="spec-card-value">{{ gpu.memorySize }}</div>
											</div>
										</div>
										<div class="spec-card">
											<div class="spec-card-icon driver-icon">
												<i class="el-icon-setting"></i>
											</div>
											<div class="spec-card-content">
												<div class="spec-card-label">驱动版本</div>
												<div class="spec-card-value">{{ gpu.driverVersion || 'N/A' }}</div>
											</div>
										</div>
										<div class="spec-card" v-if="gpu.videoModeDescription">
											<div class="spec-card-icon resolution-icon">
												<i class="el-icon-full-screen"></i>
											</div>
											<div class="spec-card-content">
												<div class="spec-card-label">分辨率</div>
												<div class="spec-card-value">{{ gpu.videoModeDescription }}</div>
											</div>
										</div>
									</div>
								</div>
							</el-collapse-item>
						</el-collapse>
					</div>
				</el-card>
			</el-col>
		</el-row>

		<!-- 网络信息 -->
		<el-row :gutter="16" style="margin-bottom: 16px">
			<el-col :span="24">
				<el-card class="network-card" shadow="hover" v-loading="systemInfoLoading">
					<template #header>
						<div class="card-header">
							<i class="el-icon-connection"></i>
							<span>网络适配器</span>
							<span class="status-text">{{ getActiveNetworks().length }} 个活跃</span>
						</div>
					</template>
					<div class="network-content">
						<el-collapse v-model="activeNetworkNames" accordion>
							<el-collapse-item v-for="network in getActiveNetworks()" :key="network.name" :name="network.name">
								<template #title>
									<div class="network-title">
										<div class="network-status">
											<div class="network-type-icon">
												<i :class="getNetworkTypeIcon(network.type)"></i>
											</div>
											<div class="network-info-summary">
												<div class="network-name">{{ network.name }}</div>
												<div class="network-speed">{{ network.speed }}</div>
											</div>
										</div>
										<div class="network-badges">
											<el-tag size="small" :type="getNetworkTypeColor(network.type)" class="type-tag">
												{{ getNetworkTypeText(network.type) }}
											</el-tag>
											<div class="status-indicator" :class="'status-' + network.operationalStatus.toLowerCase()">
												<i :class="getNetworkStatusIcon(network.operationalStatus)"></i>
												<span>{{ network.operationalStatus }}</span>
											</div>
										</div>
									</div>
								</template>
								<div class="network-details">
									<div class="network-unified-card">
										<div class="network-specs-grid">
											<div class="network-spec-item">
												<div class="network-spec-icon">
													<i class="el-icon-document"></i>
												</div>
												<div class="network-spec-content">
													<div class="network-spec-label">描述</div>
													<div class="network-spec-value">{{ network.description }}</div>
												</div>
											</div>
											<div class="network-spec-item">
												<div class="network-spec-icon">
													<i class="el-icon-postcard"></i>
												</div>
												<div class="network-spec-content">
													<div class="network-spec-label">物理地址</div>
													<div class="network-spec-value">{{ network.physicalAddress || 'N/A' }}</div>
												</div>
											</div>
											<div class="network-spec-item" v-if="network.iPv4Addresses && network.iPv4Addresses.length > 0">
												<div class="network-spec-icon">
													<i class="el-icon-location"></i>
												</div>
												<div class="network-spec-content">
													<div class="network-spec-label">IPv4地址</div>
													<div class="ip-addresses">
														<el-tag v-for="ip in network.iPv4Addresses" :key="ip.address" size="small" class="ip-tag" type="info">
															{{ ip.address }}
														</el-tag>
													</div>
												</div>
											</div>
											<div class="network-spec-item" v-if="network.statistics">
												<div class="network-spec-icon receive-icon">
													<i class="el-icon-download"></i>
												</div>
												<div class="network-spec-content">
													<div class="network-spec-label">接收数据</div>
													<div class="network-spec-value">{{ formatBytes(parseInt(network.statistics.bytesReceived || '0', 10)) }}</div>
													<div class="network-spec-extra">{{ parseInt(network.statistics.packetsReceived || '0', 10).toLocaleString() }} 包</div>
												</div>
											</div>
											<div class="network-spec-item" v-if="network.statistics">
												<div class="network-spec-icon send-icon">
													<i class="el-icon-upload"></i>
												</div>
												<div class="network-spec-content">
													<div class="network-spec-label">发送数据</div>
													<div class="network-spec-value">{{ formatBytes(parseInt(network.statistics.bytesSent || '0', 10)) }}</div>
													<div class="network-spec-extra">{{ parseInt(network.statistics.packetsSent || '0', 10).toLocaleString() }} 包</div>
												</div>
											</div>
										</div>
									</div>
								</div>
							</el-collapse-item>
						</el-collapse>
					</div>
				</el-card>
			</el-col>
		</el-row>

		<!-- 主板信息 -->
		<el-row :gutter="16" style="margin-bottom: 16px">
			<el-col :span="24">
				<el-card class="board-card" shadow="hover" v-loading="systemInfoLoading">
					<template #header>
						<div class="card-header">
							<i class="el-icon-cpu"></i>
							<span>主板信息</span>
						</div>
					</template>
					<div class="board-content">
						<div class="board-item">
							<span class="detail-label">制造商：</span>
							<span class="detail-value">{{ deviceInfo.boardInfo.manufacturer }}</span>
						</div>
						<div class="board-item">
							<span class="detail-label">产品型号：</span>
							<span class="detail-value">{{ deviceInfo.boardInfo.product }}</span>
						</div>
						<div class="board-item">
							<span class="detail-label">版本：</span>
							<span class="detail-value">{{ deviceInfo.boardInfo.version }}</span>
						</div>
						<div class="board-item">
							<span class="detail-label">序列号：</span>
							<span class="detail-value">{{ deviceInfo.boardInfo.serialNumber }}</span>
						</div>
					</div>
				</el-card>
			</el-col>
		</el-row>

		<!-- 系统信息 -->
		<el-row :gutter="16">
			<el-col :span="24">
				<el-card class="system-card" shadow="hover" v-loading="systemInfoLoading">
					<template #header>
						<div class="card-header">
							<i class="el-icon-setting"></i>
							<span>系统信息</span>
						</div>
					</template>
					<div class="system-content">
						<div class="system-item">
							<span class="detail-label">操作系统：</span>
							<span class="detail-value">{{ deviceInfo.systemInfo.osDescription }}</span>
						</div>
						<div class="system-item">
							<span class="detail-label">系统版本：</span>
							<span class="detail-value">{{ deviceInfo.systemInfo.osVersion }}</span>
						</div>
						<div class="system-item">
							<span class="detail-label">系统架构：</span>
							<span class="detail-value">{{ deviceInfo.systemInfo.osArchitecture }}</span>
						</div>
						<div class="system-item">
							<span class="detail-label">运行框架：</span>
							<span class="detail-value">{{ deviceInfo.systemInfo.frameworkDescription }}</span>
						</div>
						<div class="system-item">
							<span class="detail-label">机器名称：</span>
							<span class="detail-value">{{ deviceInfo.systemInfo.machineName }}</span>
						</div>
						<div class="system-item">
							<span class="detail-label">当前用户：</span>
							<span class="detail-value">{{ deviceInfo.systemInfo.userName }}</span>
						</div>
						<div class="system-item">
							<span class="detail-label">系统启动时间：</span>
							<span class="detail-value">{{ deviceInfo.systemInfo.systemStartTime }}</span>
						</div>
						<div class="system-item">
							<span class="detail-label">进程启动时间：</span>
							<span class="detail-value">{{ deviceInfo.systemInfo.processStartTime }}</span>
						</div>
						<div class="system-item" v-if="deviceInfo.systemInfo.processUptime">
							<span class="detail-label">进程运行时间：</span>
							<span class="detail-value">{{ deviceInfo.systemInfo.processUptime }}</span>
						</div>
						<div class="system-item" v-if="deviceInfo.systemInfo.workingSet">
							<span class="detail-label">工作集内存：</span>
							<span class="detail-value">{{ deviceInfo.systemInfo.workingSet }}</span>
						</div>
						<div class="system-item" v-if="deviceInfo.systemInfo.clrVersion">
							<span class="detail-label">CLR版本：</span>
							<span class="detail-value">{{ deviceInfo.systemInfo.clrVersion }}</span>
						</div>
					</div>
				</el-card>
			</el-col>
		</el-row>

		<!-- NuGet包信息 -->
		<el-row :gutter="16">
			<el-col :span="24">
				<el-card class="nuget-card" shadow="hover" v-loading="nugetPackagesLoading" element-loading-text="正在获取NuGet包信息...">
					<template #header>
						<div class="card-header">
							<i class="el-icon-collection"></i>
							<span>NuGet 包信息</span>
							<span class="status-text">{{ deviceInfo.nugetPackages.length }} 个包</span>
						</div>
					</template>
					<div class="nuget-content">
						<div v-for="pkg in deviceInfo.nugetPackages" :key="pkg.packageName" class="package-item">
							<el-tag round>
								<div class="package-info">
									<div class="package-name">{{ pkg.packageName }}</div>
									<div class="package-version">v{{ pkg.packageVersion }}</div>
								</div>
							</el-tag>
						</div>
					</div>
				</el-card>
			</el-col>
		</el-row>
	</div>
</template>

<script lang="ts" setup name="deviceInfo">
import { ref, reactive, onMounted } from 'vue';
import { getAPI } from '/@/utils/axios-utils';
import { SysServerApi } from '/@/api-services';

// 网络接口类型定义
interface NetworkInfo {
	name: string;
	description: string;
	type: string;
	operationalStatus: string;
	speed: string;
	physicalAddress: string;
	supportsMulticast: boolean;
	isReceiveOnly: boolean;
	dnsAddresses: string[];
	gatewayAddresses: string[];
	dhcpServerAddresses: string[];
	iPv4Addresses: Array<{
		address: string;
		subnetMask: string;
		prefixLength: number;
	}>;
	iPv6Addresses: Array<{
		address: string;
		subnetMask: string;
		prefixLength: number;
	}>;
	statistics?: {
		bytesReceived: number;
		bytesSent: number;
		packetsReceived: number;
		packetsSent: number;
		incomingPacketsDiscarded: number;
		outgoingPacketsDiscarded: number;
		incomingPacketsWithErrors: number;
		outgoingPacketsWithErrors: number;
	};
}

// 设备信息数据
const deviceInfo = reactive<any>({
	cpuInfo: {
		processorName: '',
		processorArchitecture: '',
		physicalCoreCount: 0,
		logicalCoreCount: 0,
		baseClockSpeed: 0,
		cacheSize: '',
		usagePercentage: 0,
	},
	ramInfo: {
		totalSpace: '',
		usedSpace: '',
		freeSpace: '',
		usagePercentage: 0,
	},
	diskInfos: [],
	networkInfos: [],
	gpuInfos: [],
	boardInfo: {
		manufacturer: '',
		product: '',
		version: '',
		serialNumber: '',
	},
	systemUptime: '',
	systemInfo: {
		osDescription: '',
		osVersion: '',
		osArchitecture: '',
		frameworkDescription: '',
		machineName: '',
		userName: '',
		systemStartTime: '',
		processStartTime: '',
		processUptime: '',
		workingSet: '',
		clrVersion: '',
	},
	nugetPackages: [],
});

const activeNetworkNames = ref('');
const activeGpuNames = ref('');
const systemInfoLoading = ref(false);
const nugetPackagesLoading = ref(false);

// 格式化字节数为可读格式
const formatBytes = (bytes: number) => {
	if (bytes === 0) return '0 B';
	const k = 1024;
	const sizes = ['B', 'KB', 'MB', 'GB', 'TB'];
	const i = Math.floor(Math.log(bytes) / Math.log(k));
	return parseFloat((bytes / Math.pow(k, i)).toFixed(2)) + ' ' + sizes[i];
};

// 获取系统硬件信息
const getSystemInfo = async () => {
	try {
		systemInfoLoading.value = true;
		const systemRes = await getAPI(SysServerApi).apiSysServerSystemInfoPost();
		const systemData = systemRes.data.result;

		if (systemData) {
			// 处理CPU信息
			Object.assign(deviceInfo.cpuInfo, {
				processorName: systemData.cpuInfo?.processorName || '',
				processorArchitecture: systemData.cpuInfo?.processorArchitecture || '',
				physicalCoreCount: systemData.cpuInfo?.physicalCoreCount || 0,
				logicalCoreCount: systemData.cpuInfo?.logicalCoreCount || 0,
				baseClockSpeed: systemData.cpuInfo?.baseClockSpeed || 0,
				cacheSize: formatBytes(Number(systemData.cpuInfo?.cacheBytes || '0')),
				usagePercentage: systemData.cpuInfo?.usagePercentage || 0,
			});

			// 处理内存信息
			Object.assign(deviceInfo.ramInfo, {
				totalSpace: formatBytes(Number(systemData.ramInfo?.totalBytes || '0')),
				usedSpace: formatBytes(Number(systemData.ramInfo?.usedBytes || '0')),
				freeSpace: formatBytes(Number(systemData.ramInfo?.freeBytes || '0')),
				usagePercentage: systemData.ramInfo?.usagePercentage || 0,
			});

			// 处理磁盘信息
			deviceInfo.diskInfos = (systemData.diskInfos || []).map((disk: any) => {
				const totalSpace = parseInt(disk.totalSpace || '0', 10);
				const usedSpace = parseInt(disk.usedSpace || '0', 10);
				const freeSpace = parseInt(disk.freeSpace || '0', 10);
				return {
					diskName: disk.diskName || '',
					typeName: disk.typeName || '',
					totalSpace: formatBytes(totalSpace),
					usedSpace: formatBytes(usedSpace),
					freeSpace: formatBytes(freeSpace),
					availableRate: disk.availableRate || 0,
					usedPercentage: totalSpace > 0 ? ((usedSpace / totalSpace) * 100).toFixed(1) : '0.0',
				};
			});

			// 处理网络信息
			deviceInfo.networkInfos = systemData.networkInfos || [];

			// 处理GPU信息
			deviceInfo.gpuInfos = (systemData.gpuInfos || []).map((gpu: any) => {
				// 处理分辨率字符串，移除乱码
				let resolution = gpu.videoModeDescription || '';
				if (resolution) {
					// 提取分辨率信息，过滤掉乱码
					const resolutionMatch = resolution.match(/(\d+)\s*x\s*(\d+)/);
					if (resolutionMatch) {
						resolution = `${resolutionMatch[1]} × ${resolutionMatch[2]}`;
					} else {
						resolution = '未知分辨率';
					}
				}

				return {
					name: gpu.name || '',
					description: gpu.description || '',
					vendor: gpu.vendor || '',
					deviceId: gpu.deviceId || '',
					busInfo: gpu.busInfo || '',
					driverVersion: gpu.driverVersion || '',
					memorySize: gpu.memoryBytes ? formatBytes(parseInt(gpu.memoryBytes, 10)) : 'N/A',
					videoModeDescription: resolution,
					status: gpu.status || '',
				};
			});

			// 处理主板信息
			Object.assign(deviceInfo.boardInfo, {
				manufacturer: systemData.boardInfo?.manufacturer || '',
				product: systemData.boardInfo?.product || '',
				version: systemData.boardInfo?.version || '',
				serialNumber: systemData.boardInfo?.serialNumber || '',
			});

			// 处理系统运行时间和其他系统信息
			deviceInfo.systemUptime = systemData.runtimeInfo?.systemUptime || systemData.runtimeInfo?.processUptime || '';

			// 添加系统信息到deviceInfo（可选）
			deviceInfo.systemInfo = {
				osDescription: systemData.runtimeInfo?.osDescription || '',
				osVersion: systemData.runtimeInfo?.osVersion || '',
				osArchitecture: systemData.runtimeInfo?.osArchitecture || '',
				frameworkDescription: systemData.runtimeInfo?.frameworkDescription || '',
				machineName: systemData.runtimeInfo?.machineName || '',
				userName: systemData.runtimeInfo?.userName || '',
				systemStartTime: systemData.runtimeInfo?.systemStartTime || '',
				processStartTime: systemData.runtimeInfo?.processStartTime || '',
				processUptime: systemData.runtimeInfo?.processUptime || '',
				workingSet: systemData.runtimeInfo?.workingSet ? formatBytes(Number(systemData.runtimeInfo.workingSet)) : '',
				clrVersion: systemData.runtimeInfo?.clrVersion || '',
			};
		}
	} catch (error) {
		console.error('获取系统信息失败:', error);
		// 设置默认值，避免页面崩溃
		deviceInfo.systemUptime = '获取失败';
	} finally {
		systemInfoLoading.value = false;
	}
};

// 获取NuGet包信息
const getNuGetPackagesInfo = async () => {
	try {
		nugetPackagesLoading.value = true;
		const nugetRes = await getAPI(SysServerApi).apiSysServerNuGetPackagesInfoPost();
		const nugetPackages = nugetRes.data.result;

		// 处理 NuGet 包信息
		deviceInfo.nugetPackages = (nugetPackages || []).map((pkg: any) => ({
			packageName: pkg.packageName || '',
			packageVersion: pkg.packageVersion || '',
		}));
	} catch (error) {
		console.error('获取NuGet包信息失败:', error);
	} finally {
		nugetPackagesLoading.value = false;
	}
};

// 获取活跃网络
const getActiveNetworks = () => {
	return deviceInfo.networkInfos.filter(
		(network: NetworkInfo) =>
			network.operationalStatus === 'Up' &&
			!network.name.includes('WFP') &&
			!network.name.includes('QoS') &&
			!network.name.includes('Filter') &&
			!network.name.includes('vSwitch') &&
			!network.name.includes('Loopback') &&
			!network.name.includes('Virtual') &&
			!network.name.includes('vEthernet') &&
			(network.type === 'Ethernet' || network.type === 'Wireless80211')
	);
};

// 获取网络状态图标
const getNetworkStatusIcon = (status: string) => {
	switch (status) {
		case 'Up':
			return 'el-icon-success network-status-up';
		case 'Down':
			return 'el-icon-error network-status-down';
		default:
			return 'el-icon-warning network-status-unknown';
	}
};

// 获取网络类型颜色
const getNetworkTypeColor = (type: string) => {
	switch (type) {
		case 'Ethernet':
			return 'primary';
		case 'Wireless80211':
			return 'success';
		case 'Loopback':
			return 'info';
		default:
			return 'warning';
	}
};

// 获取网络类型图标
const getNetworkTypeIcon = (type: string) => {
	switch (type) {
		case 'Ethernet':
			return 'el-icon-connection';
		case 'Wireless80211':
			return 'el-icon-wifi';
		case 'Loopback':
			return 'el-icon-refresh';
		default:
			return 'el-icon-link';
	}
};

// 获取网络类型文本
const getNetworkTypeText = (type: string) => {
	switch (type) {
		case 'Ethernet':
			return '有线网络';
		case 'Wireless80211':
			return '无线网络';
		case 'Loopback':
			return '环回接口';
		default:
			return type;
	}
};

// 获取CPU使用率颜色
const getCpuColor = (percentage: number) => {
	if (percentage < 50) return '#67c23a';
	if (percentage < 80) return '#e6a23c';
	return '#f56c6c';
};

// 获取内存使用率颜色
const getRamColor = (percentage: number) => {
	if (percentage < 60) return '#409eff';
	if (percentage < 80) return '#e6a23c';
	return '#f56c6c';
};

// 获取磁盘使用率颜色
const getDiskColor = (availablePercentage: number) => {
	const usedPercentage = 100 - availablePercentage;
	if (usedPercentage < 70) return '#67c23a';
	if (usedPercentage < 90) return '#e6a23c';
	return '#f56c6c';
};

onMounted(async () => {
	console.log('设备信息页面已加载');
	// 先快速加载NuGet包信息
	await getNuGetPackagesInfo();
	// 然后加载可能耗时的系统信息
	await getSystemInfo();
});
</script>

<style lang="scss" scoped>
.device-info-container {
	padding: 16px;
	background: #f5f7fa;
	min-height: 100vh;
}

.card-header {
	display: flex;
	align-items: center;
	justify-content: space-between;
	font-weight: 600;

	i {
		margin-right: 8px;
		font-size: 18px;
	}

	> span:first-of-type {
		flex: 1;
	}
}

.status-text {
	font-size: 12px;
	color: #67c23a;
	flex-shrink: 0;
}

// 概览卡片
.overview-card {
	background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
	color: white;

	:deep(.el-card__header) {
		background: rgba(255, 255, 255, 0.1);
		border-bottom: 1px solid rgba(255, 255, 255, 0.2);
		color: white;
	}
}

.overview-content {
	display: flex;
	justify-content: space-around;
	flex-wrap: wrap;
	gap: 20px;
}

.overview-item {
	display: flex;
	align-items: center;
	gap: 16px;
	min-width: 200px;
	flex: 1;
	min-height: 80px;
	max-height: 103px;
}

.overview-icon {
	width: 60px;
	height: 60px;
	border-radius: 50%;
	display: flex;
	align-items: center;
	justify-content: center;
	font-size: 24px;
	background: rgba(255, 255, 255, 0.2);
	flex-shrink: 0;
}

.overview-text {
	flex: 1;
	min-width: 0;
	display: flex;
	flex-direction: column;
	justify-content: center;
	min-height: 72px;
	max-height: 95px;

	.overview-title {
		font-size: 14px;
		opacity: 0.8;
		margin-bottom: 4px;
		line-height: 1.2;
		height: 17px;
		display: flex;
		align-items: center;
	}

	.overview-value {
		font-size: 18px;
		font-weight: 600;
		margin-bottom: 4px;
		word-break: break-word;
		line-height: 1.3;
		min-height: 23px;
		max-height: 46px;
		display: -webkit-box;
		line-clamp: 2;
		-webkit-line-clamp: 2;
		-webkit-box-orient: vertical;
		overflow: hidden;
		text-overflow: ellipsis;
	}

	.overview-subtitle {
		font-size: 12px;
		opacity: 0.7;
		line-height: 1.2;
		height: 15px;
		display: flex;
		align-items: center;
		overflow: hidden;
		text-overflow: ellipsis;
		white-space: nowrap;
	}
}

// 性能卡片
.performance-card {
	&.cpu-card {
		border-left: 4px solid #e6a23c;
	}

	&.ram-card {
		border-left: 4px solid #409eff;
	}
}

.performance-content {
	display: flex;
	align-items: center;
	gap: 24px;
}

.performance-chart {
	flex-shrink: 0;
}

.progress-content {
	text-align: center;

	.progress-value {
		font-size: 24px;
		font-weight: 600;
		color: #303133;
	}

	.progress-label {
		font-size: 12px;
		color: #909399;
		margin-top: 4px;
	}
}

.performance-details {
	flex: 1;
}

.detail-item {
	display: flex;
	justify-content: space-between;
	padding: 8px 0;
	border-bottom: 1px solid #f0f0f0;

	&:last-child {
		border-bottom: none;
	}
}

.detail-label {
	color: #606266;
	font-size: 14px;
}

.detail-value {
	color: #303133;
	font-weight: 500;
	font-size: 14px;
}

// 磁盘卡片
.disk-card {
	border-left: 4px solid #67c23a;
}

.disk-item {
	background: #f8f9fa;
	border-radius: 8px;
	padding: 16px;
	margin-bottom: 16px;
	border: 1px solid #e4e7ed;

	&:last-child {
		margin-bottom: 0;
	}
}

.disk-content {
	display: flex;
	align-items: center;
	gap: 20px;
}

.disk-chart {
	flex-shrink: 0;
}

.disk-progress-content {
	text-align: center;

	.disk-usage {
		font-size: 14px;
		font-weight: 600;
		color: #303133;
	}
}

.disk-all-info {
	flex: 1;
	display: flex;
	flex-direction: column;
	gap: 12px;
}

.disk-title-section {
	display: flex;
	align-items: center;
	justify-content: space-between;
	margin-bottom: 2px;

	.disk-name {
		font-size: 16px;
		font-weight: 600;
		color: #303133;
		line-height: 1.2;
	}

	.disk-type {
		font-size: 11px;
		color: #909399;
		background: #e4e7ed;
		padding: 2px 8px;
		border-radius: 12px;
		display: inline-block;
	}
}

.disk-capacity-info {
	display: flex;
	flex-direction: column;
	gap: 6px;
}

.capacity-item {
	display: flex;
	justify-content: space-between;
	align-items: center;

	.capacity-label {
		font-size: 12px;
		color: #606266;
	}

	.capacity-value {
		font-size: 13px;
		font-weight: 600;
		color: #303133;
	}
}

// GPU卡片
.gpu-card {
	border-left: 4px solid #f56c6c;
}

.gpu-title {
	display: flex;
	justify-content: space-between;
	align-items: center;
	width: 100%;
	padding-right: 16px;
}

.gpu-status {
	display: flex;
	align-items: center;
	gap: 12px;
}

.gpu-type-icon {
	width: 32px;
	height: 32px;
	border-radius: 50%;
	display: flex;
	align-items: center;
	justify-content: center;
	background: #f0f2f5;
	color: #f56c6c;
	font-size: 16px;
}

.gpu-info-summary {
	.gpu-name {
		font-weight: 600;
		color: #303133;
		font-size: 15px;
		line-height: 1.2;
	}

	.gpu-memory {
		font-size: 12px;
		color: #909399;
		margin-top: 2px;
	}
}

.gpu-badges {
	display: flex;
	align-items: center;
	gap: 12px;
}

.driver-info {
	.driver-text {
		font-size: 12px;
		color: #606266;
	}
}

.gpu-details {
	padding: 16px 0 0 0;
}

.gpu-specs-grid {
	display: grid;
	grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
	gap: 12px;
}

.spec-card {
	background: #f8f9fa;
	border-radius: 8px;
	padding: 12px;
	display: flex;
	align-items: center;
	gap: 10px;
	border: 1px solid #e4e7ed;
}

.spec-card-icon {
	width: 32px;
	height: 32px;
	border-radius: 50%;
	display: flex;
	align-items: center;
	justify-content: center;
	font-size: 14px;
	flex-shrink: 0;

	&.memory-icon {
		background: rgba(103, 194, 58, 0.1);
		color: #67c23a;
	}

	&.driver-icon {
		background: rgba(64, 158, 255, 0.1);
		color: #409eff;
	}

	&.resolution-icon {
		background: rgba(230, 162, 60, 0.1);
		color: #e6a23c;
	}
}

.spec-card-content {
	flex: 1;

	.spec-card-label {
		font-size: 12px;
		color: #909399;
		margin-bottom: 2px;
	}

	.spec-card-value {
		font-size: 13px;
		font-weight: 600;
		color: #303133;
	}
}

// 网络卡片
.network-card {
	border-left: 4px solid #909399;
}

.network-title {
	display: flex;
	justify-content: space-between;
	align-items: center;
	width: 100%;
	padding-right: 16px;
}

.network-status {
	display: flex;
	align-items: center;
	gap: 12px;
}

.network-type-icon {
	width: 40px;
	height: 40px;
	border-radius: 50%;
	display: flex;
	align-items: center;
	justify-content: center;
	background: #f0f2f5;
	color: #409eff;
	font-size: 18px;
}

.network-info-summary {
	.network-name {
		font-weight: 600;
		color: #303133;
		font-size: 16px;
		line-height: 1.2;
	}

	.network-speed {
		font-size: 12px;
		color: #909399;
		margin-top: 2px;
	}
}

.network-badges {
	display: flex;
	align-items: center;
	gap: 8px;
}

.type-tag {
	height: 24px;
	line-height: 22px;
}

.status-indicator {
	display: flex;
	align-items: center;
	gap: 3px;
	padding: 0 6px;
	border-radius: 4px;
	font-size: 12px;
	font-weight: 500;
	height: 24px;
	line-height: 1;

	&.status-up {
		background: rgba(103, 194, 58, 0.1);
		color: #67c23a;
	}

	&.status-down {
		background: rgba(245, 108, 108, 0.1);
		color: #f56c6c;
	}

	&.status-unknown {
		background: rgba(230, 162, 60, 0.1);
		color: #e6a23c;
	}
}

.network-details {
	padding: 16px 0 0 0;
}

.network-unified-card {
	background: #f8f9fa;
	border-radius: 8px;
	padding: 16px;
}

.network-specs-grid {
	display: grid;
	grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
	gap: 12px;
}

.network-spec-item {
	display: flex;
	align-items: flex-start;
	gap: 10px;
	background: #fff;
	padding: 12px;
	border-radius: 6px;
	border: 1px solid #e4e7ed;
}

.network-spec-icon {
	width: 32px;
	height: 32px;
	border-radius: 50%;
	display: flex;
	align-items: center;
	justify-content: center;
	font-size: 14px;
	flex-shrink: 0;
	background: rgba(64, 158, 255, 0.1);
	color: #409eff;

	&.receive-icon {
		background: rgba(103, 194, 58, 0.1);
		color: #67c23a;
	}

	&.send-icon {
		background: rgba(245, 108, 108, 0.1);
		color: #f56c6c;
	}
}

.network-spec-content {
	flex: 1;

	.network-spec-label {
		font-size: 12px;
		color: #909399;
		margin-bottom: 2px;
	}

	.network-spec-value {
		font-size: 14px;
		color: #303133;
		font-weight: 500;
		line-height: 1.3;
	}

	.network-spec-extra {
		font-size: 11px;
		color: #909399;
		margin-top: 2px;
	}
}

.ip-addresses {
	margin-top: 4px;
}

.ip-tag {
	margin-right: 6px;
	margin-bottom: 4px;
}

// 主板卡片
.board-card {
	border-left: 4px solid #973399;
}

.board-content {
	display: grid;
	grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
	gap: 16px;
}

.board-item {
	display: flex;
	justify-content: space-between;
	padding: 12px;
	background: #f8f9fa;
	border-radius: 6px;
}

// 系统信息卡片
.system-card {
	border-left: 4px solid #409eff;
}

.system-content {
	display: grid;
	grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
	gap: 16px;
}

.system-item {
	display: flex;
	justify-content: space-between;
	padding: 12px;
	background: #f8f9fa;
	border-radius: 6px;
}

// 响应式设计
@media (max-width: 768px) {
	.overview-content {
		flex-direction: column;
	}

	.overview-item {
		min-width: 100%;
	}

	.performance-content {
		flex-direction: column;
		text-align: center;
	}

	.disk-content {
		flex-direction: column;
		text-align: center;
	}

	.stats-grid {
		grid-template-columns: 1fr;
	}

	.board-content {
		grid-template-columns: 1fr;
	}
}

// 动画效果
.el-card {
	transition:
		transform 0.3s ease,
		box-shadow 0.3s ease;

	&:hover {
		transform: translateY(-2px);
		box-shadow: 0 8px 25px rgba(0, 0, 0, 0.1);
	}
}

.el-progress {
	:deep(.el-progress__text) {
		color: inherit !important;
	}
}

:deep(.el-collapse-item__header) {
	padding-left: 0;
	padding-right: 16px;
}

:deep(.el-collapse-item__content) {
	padding-bottom: 16px;
}

// NuGet包信息卡片样式
.nuget-card {
	border-left: 4px solid #409eff;
	margin-top: 16px;
}

.nuget-content {
	padding: 8px;
	display: flex;
	flex-wrap: wrap;
	gap: 8px;
}

.package-item {
	display: inline-block;
	margin: 4px;
	text-align: left;
}

.package-info {
	display: inline-flex;
	align-items: center;
	gap: 4px;
}

.package-name {
	font-family: 'Consolas', monospace;
}

.package-version {
	color: #909399;
	font-size: 9px;
}

:deep(.el-tag) {
	--el-tag-bg-color: var(--el-color-primary-light-9);
	--el-tag-border-color: var(--el-color-primary-light-8);
	--el-tag-hover-color: var(--el-color-primary);

	&:hover {
		background-color: var(--el-color-primary-light-8);
	}
}
</style>
