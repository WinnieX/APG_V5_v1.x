#include <Windows.h>
#include <tchar.h>

#include <stdio.h>
#include <shlobj.h>
#include <Shlwapi.h>

#pragma comment(lib, "shell32.lib")
#pragma comment(lib,"shlwapi.lib")

#include <vector>

#include "md5.h"
#include "resource.h"
#include "ico.h"

//#define M_IAP

#ifndef M_IAP
#define DEVICE_MODEL APG
#define EXE_FILE_NAME AEC.exe
#define CONFIG_FILE_NAME AEC.exe.config
#define TARGET_EXE_FILE_NAME DEVICE_MODEL EXE_FILE_NAME
#define TARGET_CONFIG_FILE_NAME DEVICE_MODEL CONFIG_FILE_NAME
#else
#define DEVICE_MODEL M-IAP
#define EXE_FILE_NAME AEC.exe
#define CONFIG_FILE_NAME AEC.exe.config
#define TARGET_EXE_FILE_NAME M-IAP.exe
#define TARGET_CONFIG_FILE_NAME M-IAP.exe.config
#endif
//#define DEVICE_MODEL M-Audio


#define STRINGIFY(x) #x
#define STRINGIFY_EXPANDED(x) STRINGIFY(x)

TCHAR szProductName[] = _T("AEC");
TCHAR szModelName[] = _T(STRINGIFY_EXPANDED(DEVICE_MODEL));

TCHAR szTargetExeFileName[] = _T(STRINGIFY_EXPANDED(TARGET_EXE_FILE_NAME));
TCHAR szTargetExeConfigFileName[] = _T(STRINGIFY_EXPANDED(TARGET_CONFIG_FILE_NAME));
TCHAR szlibLog4netFileName[] = _T("log4net.dll");
TCHAR szLibfftwFileName[] = _T("libfftw3-3.dll");
TCHAR szLibDXDSName[] = _T("Microsoft.DirectX.DirectSound.dll");
TCHAR szLibDXName[] = _T("Microsoft.DirectX.dll");
TCHAR szZhHelpFileName[] = _T("AEC_zh-CN.chm");
TCHAR szEnHelpFileName[] = _T("AEC_en-US.chm");
TCHAR szLibPBName[] = _T("Google.ProtocolBuffers.dll");
TCHAR szLibNewtonJsonName[] = _T("Newtonsoft.Json.dll");
TCHAR szSPCExeFileName[] = _T("SerialPortClient.exe");
TCHAR szSPCConfigFileName[] = _T("SerialPortClient.exe.config");

TCHAR szIllegallyTamperedError[] = _T("检测到程序遭到非法篡改，无法继续执行!\r\nThe program was detected to be illegally tampered with and could not proceed.!");
TCHAR szProgramError[] = _T("程序出现错误, 请重试!\r\nAn error occurred in the program, Please try again!");
TCHAR szProgramIsRunningError[] = _T("AEC程序正在运行中，请先关闭！\r\nThe AEC program is running, Please close it first!");

bool  isRunning = false;
TCHAR szMainExcPath[MAX_PATH] = { 0 };
TCHAR targetDir[MAX_PATH] = { 0 };

int Execute();

int _tmain(int argc, _TCHAR* argv[])
{
	return Execute();
}

int APIENTRY _tWinMain(_In_ HINSTANCE hInstance,
	_In_opt_ HINSTANCE hPrevInstance,
	_In_ LPTSTR    lpCmdLine,
	_In_ int       nCmdShow)
{
	return Execute();
}

class Resource
{
public:
	Resource(int resourceId, LPCTSTR type)
	{
		HRSRC hRsrc = FindResource(NULL, MAKEINTRESOURCE(resourceId), type);
		if (hRsrc == NULL)
		{
			return;
		}
		DWORD dwSize = SizeofResource(NULL, hRsrc);
		if (dwSize == 0)
		{
			return;
		}
		HGLOBAL hGlobal = LoadResource(NULL, hRsrc);
		if (NULL == hGlobal)
		{
			return;
		}

		this->buffer = (byte *)LockResource(hGlobal);
		this->size = SizeofResource(NULL, hRsrc);
	}

public:
	byte *buffer = NULL;
	unsigned long size = 0;
};

class BinResource : public Resource
{
public:
	static BinResource InitResource(int resourceId, LPCTSTR target, bool needToCheck = false)
	{
		//BinResource resource;
		//HRSRC hRsrc = FindResource(NULL, MAKEINTRESOURCE(resourceId), TEXT("Binary"));
		//if (hRsrc == NULL)
		//{
		//	return resource;
		//}
		//DWORD dwSize = SizeofResource(NULL, hRsrc);
		//if (dwSize == 0)
		//{
		//	return resource;
		//}
		//HGLOBAL hGlobal = LoadResource(NULL, hRsrc);
		//if (NULL == hGlobal)
		//{
		//	return resource;
		//}

		//resource.buffer = LockResource(hGlobal);
		//resource.size = SizeofResource(NULL, hRsrc);
		//lstrcpyW(resource.targetPath, target);
		//resource.isNeedToCheck = needToCheck;

		//return resource;

		return BinResource(resourceId, target, needToCheck);
	}

public:
	BinResource(int resourceId, LPCTSTR target, bool needToCheck = false) : Resource(resourceId, TEXT("Binary"))
	{
		lstrcpyW(this->targetPath, target);
		this->isNeedToCheck = needToCheck;
	}

	bool IsValid()
	{
		return buffer != NULL && size > 0 && lstrlen(targetPath) > 0;
	}

public:
	TCHAR targetPath[MAX_PATH];
	bool isNeedToCheck = false;
};

void UpdateTargetFilePath(LPTSTR path, LPCTSTR targetDir, LPCTSTR fileName)
{
	lstrcpyW(path, targetDir);
	lstrcatW(path, fileName);
}

const wchar_t* wcstrrchr(const wchar_t* str, const wchar_t wc)
{
	const wchar_t* pwc = NULL;
	for (int i = wcslen(str) - 1; i >= 0; i--)
	{
		if (str[i] == wc)
		{
			pwc = str + i;
			break;
		}
	}
	return pwc;
}

bool CreateMultiDir(const wchar_t* path)
{
	if (path == NULL) return false;
	const wchar_t* pwcStrrchr = wcstrrchr(path, L'\\');
	if (!pwcStrrchr) return false;
	if (PathIsDirectory(path)) return true;

	wchar_t wsSubPath[MAX_PATH] = {};
	memset(wsSubPath, 0, sizeof(wsSubPath));
	for (int i = 0; i < pwcStrrchr - path; i++)
		wsSubPath[i] = *(path + i);
	CreateMultiDir(wsSubPath);
	if (CreateDirectory(path, NULL)) return true;
	return false;
}

const char HEX_NUMBERS[16] = {
	'0', '1', '2', '3',
	'4', '5', '6', '7',
	'8', '9', 'a', 'b',
	'c', 'd', 'e', 'f'
};

std::string BytesToHexStr(unsigned char digest[16])
{
	std::string str;
	str.reserve(16 << 1);
	for (size_t i = 0; i < 16; ++i) {
		int t = digest[i];
		int a = t / 16;
		int b = t % 16;
		str.append(1, HEX_NUMBERS[a]);
		str.append(1, HEX_NUMBERS[b]);
	}

	return str;
}

std::string MD5ToStr(const byte* buf, unsigned int len)
{
	byte digest[16] = { 0 };
	CalcMD5(buf, len, digest);
	return BytesToHexStr(digest);
}

std::string GetFileMD5(LPCTSTR path)
{
	std::string result;
	if (PathFileExists(path))
	{
		HANDLE hFile = CreateFile(path, GENERIC_READ, FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
		if (hFile == INVALID_HANDLE_VALUE)
		{
			CloseHandle(hFile);
			return "";
		}

		DWORD fileSize = GetFileSize(hFile, NULL);
		if (fileSize > 0)
		{
			byte* buffer = new byte[fileSize];
			DWORD bytesRead = 0;
			if (ReadFile(hFile, buffer, fileSize, &bytesRead, NULL) > 0 && bytesRead == fileSize)
			{
				result = MD5ToStr(buffer, fileSize);
			}
			delete[] buffer;
		}
		CloseHandle(hFile);
	}

	return result;
}

bool ExportFile(LPCTSTR path, LPVOID data, DWORD size)
{
	HANDLE hFile = CreateFile(path, GENERIC_WRITE, 0, NULL, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);
	if (hFile == INVALID_HANDLE_VALUE)
	{
		CloseHandle(hFile);
		return false;
	}

	DWORD bytesWrite = 0;
	WriteFile(hFile, data, size, &bytesWrite, NULL);
	CloseHandle(hFile);

	return true;
}

bool ExportResource(BinResource & resource, LPCTSTR targetDir, LPTSTR err)
{
	if (!PathFileExists(targetDir))
	{
		CreateMultiDir(targetDir);
	}

	if (PathFileExists(resource.targetPath))
	{
		if (resource.isNeedToCheck)
		{
			std::string fileMD5 = GetFileMD5(resource.targetPath);
			std::string resourceMD5 = MD5ToStr(resource.buffer, resource.size);
			if (fileMD5 != resourceMD5)
			{
				if (isRunning)
				{
					lstrcpyW(err, szProgramIsRunningError);
					return false;
				}

				//导出文件错误
				if (!ExportFile(resource.targetPath, resource.buffer, resource.size))
				{
					lstrcpyW(err, szProgramError);
					return false;
				}
			}
		}
	}
	else
	{
		if (!ExportFile(resource.targetPath, resource.buffer, resource.size))
		{
			lstrcpyW(err, szProgramError);
			return false;
			//导出文件错误
		}
	}

	return true;
}

bool ReleaseBinary(LPTSTR err)
{
	HANDLE hMutex = CreateMutex(NULL, false, szProductName);
	if (GetLastError() == ERROR_ALREADY_EXISTS)
	{
		isRunning = true;
		printf("mutext is exist\n");
	}
	CloseHandle(hMutex);

	TCHAR myDocuments[MAX_PATH] = { 0 };
	HRESULT result = SHGetFolderPath(NULL, CSIDL_COMMON_DOCUMENTS, NULL, SHGFP_TYPE_CURRENT, myDocuments);
	lstrcpyW(targetDir, myDocuments);
	lstrcatW(targetDir, _T("\\"));
	lstrcatW(targetDir, szProductName);
	lstrcatW(targetDir, _T("\\"));
	lstrcatW(targetDir, szModelName);
	lstrcatW(targetDir, _T("\\"));

	TCHAR aec_binTargetPath[MAX_PATH] = { 0 };
	TCHAR aec_configTargetPath[MAX_PATH] = { 0 };
	TCHAR lib_log4netPath[MAX_PATH] = { 0 };
	TCHAR lib_fftwPath[MAX_PATH] = { 0 };
	TCHAR lib_dxdsPath[MAX_PATH] = { 0 };
	TCHAR lib_dxPath[MAX_PATH] = { 0 };
	TCHAR aec_zhHelpPath[MAX_PATH] = { 0 };
	TCHAR aec_enHelpPath[MAX_PATH] = { 0 };
	TCHAR lib_protobufPath[MAX_PATH] = { 0 };
	TCHAR lib_newtonJsonPath[MAX_PATH] = { 0 };
	//TCHAR spc_binPath[MAX_PATH] = { 0 };
	//TCHAR spc_configPath[MAX_PATH] = { 0 };

	UpdateTargetFilePath(aec_binTargetPath, targetDir, szTargetExeFileName);
	UpdateTargetFilePath(aec_configTargetPath, targetDir, szTargetExeConfigFileName);
	UpdateTargetFilePath(lib_log4netPath, targetDir, szlibLog4netFileName);
	UpdateTargetFilePath(lib_fftwPath, targetDir, szLibfftwFileName);
	UpdateTargetFilePath(lib_dxdsPath, targetDir, szLibDXDSName);
	UpdateTargetFilePath(lib_dxPath, targetDir, szLibDXName);
	UpdateTargetFilePath(aec_zhHelpPath, targetDir, szZhHelpFileName);
	UpdateTargetFilePath(aec_enHelpPath, targetDir, szEnHelpFileName);
	UpdateTargetFilePath(lib_protobufPath, targetDir, szLibPBName);
	UpdateTargetFilePath(lib_newtonJsonPath, targetDir, szLibNewtonJsonName);

	/*
	UpdateTargetFilePath(spc_binPath, targetDir, szSPCExeFileName);
	UpdateTargetFilePath(spc_configPath, targetDir, szSPCConfigFileName);
	*/

#ifdef M_IAP
	bool is_check_config_file = false;
#else
	bool is_check_config_file = true;
#endif

	BinResource aec_bin = BinResource::InitResource(IDR_BINARY1, aec_binTargetPath, true);
	BinResource aec_config = BinResource::InitResource(IDR_BINARY2, aec_configTargetPath, is_check_config_file);
	BinResource lib_log4net = BinResource::InitResource(IDR_BINARY3, lib_log4netPath);
	BinResource lib_fftw = BinResource::InitResource(IDR_BINARY4, lib_fftwPath);
	BinResource lib_dxds = BinResource::InitResource(IDR_BINARY5, lib_dxdsPath);
	BinResource lib_dx = BinResource::InitResource(IDR_BINARY6, lib_dxPath);
	BinResource lib_protobuf = BinResource::InitResource(IDR_BINARY7, lib_protobufPath);
	BinResource lib_newtonJson = BinResource::InitResource(IDR_BINARY8, lib_newtonJsonPath);
	
	std::vector<BinResource> resources = { aec_bin, aec_config, lib_log4net, lib_fftw, lib_dxds, lib_dx, lib_protobuf, lib_newtonJson};
	//std::vector<BinResource> resources = { aec_bin, aec_config, lib_fftw, lib_dxds, lib_dx, spc_bin, spc_config };

	for each (BinResource resource in resources)
	{
		if (!resource.IsValid() && false)
		{
			lstrcpyW(err, szProgramError);
			return false;
		}

		if (!ExportResource(resource, targetDir, err))
		{
			return false;
		}
	}

	lstrcpyW(szMainExcPath, aec_bin.targetPath);

	if (!PathFileExists(szMainExcPath))
	{
		lstrcpyW(err, szProgramError);
		return false;
	}

	return true;
}

bool GetModuleDir(LPTSTR path)
{
	if (PathRemoveFileSpec(path))
	{
		PathAddBackslash(path);
		if (PathFileExists(path))
		{
			return true;
		}
	}

	return false;
}

const char* icon_digest = "2ced77b889512e8ce68ae51a58a442e3";

bool CheckIcon()
{
	HRSRC hRsrc = FindResource(NULL, MAKEINTRESOURCE(IDI_ICON), RT_GROUP_ICON);
	HGLOBAL hGlobal = LoadResource(NULL, hRsrc);
	if (hGlobal == NULL)
	{
		return false;
	}
	LPMEMICONDIR   lpIcon = (LPMEMICONDIR)LockResource(hGlobal);
	if (lpIcon == NULL)
	{
		return false;
	}

	LPMEMICONDIRENTRY pEntries = (LPMEMICONDIRENTRY)lpIcon->idEntries;

	if (lpIcon->idCount == 0)
	{
		return false;
	}

	MD5Context context;
	MD5Init(&context);
	
	
	for (int i = 0; i < lpIcon->idCount; i++)
	{
		HRSRC hIcon = FindResource(NULL, MAKEINTRESOURCE(pEntries[i].nID), RT_ICON);
		if (hIcon == NULL)
		{
			return false;
		}

		DWORD iconSize = SizeofResource(NULL, hIcon);
		if (iconSize == 0)
		{
			return false;
		}

		HGLOBAL hIconResource = LoadResource(NULL, hIcon);
		if (hIconResource == NULL)
		{
			return false;
		}
		BYTE* pIconData = (BYTE*)LockResource(hIconResource);
		if (pIconData == NULL)
		{
			return false;
		}

		MD5Update(&context, pIconData, iconSize);
	}

	unsigned char digestData[16] = { 0 };
	
	MD5Final(&context, digestData);

	std::string digest = BytesToHexStr(digestData);
	
	return digest == icon_digest;
}

int Execute()
{
	//if (!CheckIcon())
	//{
	//	MessageBox(NULL, szIllegallyTamperedError, _T("Error"), MB_OK | MB_ICONERROR);
	//	return 0;
	//}
	
	TCHAR szCurrModuleDir[MAX_PATH] = { 0 };
	if (GetModuleFileName(NULL, szCurrModuleDir, MAX_PATH) == 0 || !GetModuleDir(szCurrModuleDir))
	{
		MessageBox(NULL, szProgramError, _T("Error"), MB_OK | MB_ICONERROR);
		return 0;
	}

	TCHAR szErrMessage[32768] = { 0 };
	if (ReleaseBinary(szErrMessage))
	{
		STARTUPINFO si;
		PROCESS_INFORMATION pi;
		ZeroMemory(&si, sizeof(si));
		si.cb = sizeof(si);
		si.dwFlags = STARTF_USESHOWWINDOW;
		si.wShowWindow = TRUE;

		ZeroMemory(&pi, sizeof(pi));

		TCHAR szCommandLine[32768] = { 0 };
		lstrcpy(szCommandLine, _T("\""));
		lstrcat(szCommandLine, szMainExcPath);
		lstrcat(szCommandLine, _T("\" "));
		lstrcat(szCommandLine, szCurrModuleDir);
		if (CreateProcess(NULL, szCommandLine, NULL, NULL, FALSE, 0, NULL, NULL, &si, &pi))
		{
			//printf("create process 1\n");
			//WaitForSingleObject(pi.hProcess, INFINITE);
			//printf("create process 2\n");
			CloseHandle(pi.hThread);
			CloseHandle(pi.hProcess);
			//printf("create process 3\n");
		}
		else
		{
			MessageBox(NULL, szProgramError, _T("Error"), MB_OK | MB_ICONERROR);
		}
	}
	else
	{
		MessageBox(NULL, szErrMessage, _T("Error"), MB_OK | MB_ICONERROR);
	}

	return 0;
}