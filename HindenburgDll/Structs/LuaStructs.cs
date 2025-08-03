using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HindenburgDll.Structs
{
	public struct common_header
	{
		public byte tt;
		public byte marked;
		public byte memcat;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct Value
	{
		[FieldOffset(0)] public IntPtr gc;
		[FieldOffset(0)] public IntPtr p;
		[FieldOffset(0)] public double n;
		[FieldOffset(0)] public int b;
		[FieldOffset(4)] public float v2;
	}

	public struct TValue
	{
		public Value value;
		public int extra;
		public int tt;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct table_union
	{
		[FieldOffset(0)] public int lastfree;
		[FieldOffset(4)] public int aboundary;
	}

	public struct lua_Debug
	{
		public IntPtr name;      // (n)
		public IntPtr what;      // (s) `Lua', `C', `main', `tail'
		public IntPtr source;    // (s)
		public IntPtr short_src; // (s)
		public int linedefined;       // (s)
		public int currentline;       // (l)
		public char nupvals; // (u) number of upvalues
		public char nparams; // (a) number of parameters
		public char isvararg;         // (a)
		public IntPtr userdata;        // only valid in luau_callhook

		public char ssbuf;
	}

	public struct Proto
	{
		public common_header header;

		public byte nups;
		public byte numparams;
		public byte is_vararg;
		public byte maxstacksize;
		public byte flags;

		public IntPtr k;
		public IntPtr code;
		public IntPtr p;
		public IntPtr codeentry;

		public IntPtr execdata;
		public IntPtr exectarget;

		public IntPtr lineinfo;
		public IntPtr abslineinfo;
		public IntPtr locvars;
		public IntPtr upvalues;
		public IntPtr source;

		public IntPtr debugname;
		public IntPtr debuginsn;

		public IntPtr typeinfo;

		public IntPtr userdata;

		public IntPtr gclist;

		public int sizecode;
		public int sizep;
		public int sizelocvars;
		public int sizeupvalues;
		public int sizek;
		public int sizelineinfo;
		public int linegaplog2;
		public int linedefined;
		public int bytecodeid;
		public int sizetypeinfo;
	}

	public struct c_closure
	{
		public IntPtr f;
		public IntPtr cont;
		public IntPtr debugname;
		public IntPtr aboundary;
	}

	public struct l_closure
	{
		public IntPtr p;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct closures
	{
		[FieldOffset(0)] public c_closure c;
		[FieldOffset(0)] public l_closure l;
	}

	public struct table
	{
		public common_header header;

		public byte tmcache;
		public byte readonlyP;
		public byte safeenv;
		public byte lsizenode;
		public byte nodemask8;

		public int sizearray;
		public table_union union;

		public IntPtr metatable;
		public IntPtr array;
		public IntPtr node;
		public IntPtr gclist;
	}

	public struct blank_closure
	{
		public common_header header;

		public byte isC;
		public byte nupvalues;
		public byte stacksize;
		public byte preload;
		public IntPtr gcObject;
		public IntPtr env;
	}

	public struct lua_closure
	{
		public common_header header;

		public byte isC;
		public byte nupvalues;
		public byte stacksize;
		public byte preload;
		public IntPtr gcObject;
		public IntPtr env;

		public closures closures;
	}
}
