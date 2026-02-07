// src/app/dtos/attendance-detail.dto.ts

export interface AttendanceDetailDto {
    id: string;
    attendanceId: string;
    studentId: string;
    status: number;
    notes: string | null;
    createdAt: string;
}