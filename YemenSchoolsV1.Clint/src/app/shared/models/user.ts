export type User = {
    id: string;
    name: string;
    imageUrl?: string;
    email: string;
    entityId: string;
    userType: string;
    schoolId?: string;
}
export interface ChangePasswordDto {
    currentPassword: string;
    newPassword: string;
}
export interface UpdateParentProfileDto {
    name?: string;
    imageUrl?: string;
    phoneNumber?: string;
    address?: string;
    email?: string;
    jobTitle?: string;
}

