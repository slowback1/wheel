﻿export interface WheelRequest {
	wheelSetting: WheelSetting;
	options?: WheelSpinOptions;
}

export interface WheelSetting {
	name: string;
	slices: WheelSlice[];
}

export interface WheelSlice {
	label: string;
	size: number;
}

export enum WheelSpinMode {
	Random = 0,
	Rigged = 1,
	Distribution = 2
}

export interface WheelSpinOptions {
	mode: WheelSpinMode;
	riggedSlice?: number;
}

export interface SpinResult {
	wheelUsed: WheelSetting;
	sliceLanded: number;
}

export interface CreateUserRequest {
	username: string;
	password: string;
}

export interface LoginRequest {
	username: string;
	password: string;
}

export interface UserResponse {
	username: string;
}

export interface TokenResponse {
	token: string;
}
