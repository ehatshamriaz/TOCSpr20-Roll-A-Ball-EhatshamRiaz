using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace roll_a_ball_helper
{
	public class HelperClass
	{
		private Collider[] colliders;
		public bool checkSpawnPosition(Vector3 SpawnPos)
		{
			colliders = Physics.OverlapSphere(SpawnPos, 1);
			for (int i = 0; i < colliders.Length; i++)
			{
				if (colliders[i].tag == "ground")
				{
					continue;
				}
				Vector3 centerPoint = colliders[i].bounds.center;
				float width = colliders[i].bounds.extents.x;
				float height = colliders[i].bounds.extents.z;
				float leftExtent = (centerPoint.x - width) - 5;
				float rightExtent = (centerPoint.x + width) + 5;
				float upExtent = (centerPoint.z - height) - 5;
				float downExtent = (centerPoint.z + height) + 5;
				if (SpawnPos.x >= leftExtent && SpawnPos.x <= rightExtent)
				{
					if (SpawnPos.z >= upExtent && SpawnPos.z <= downExtent)
					{
						return false;
					}
				}
				
			}
			return true;
		}
		
		
		public string createPalindromeString(int size)
		{
			
			char[] str = new char[size];
			
			int end = size - 1;
			int i = 1;
			for (i = 0; i < size; i++)
			{
				int x = UnityEngine.Random.Range(0, 3);
				if (x == 0)
				{
					str[i] = 'X';
					str[end] = 'X';
				}
				else if (x == 1)
				{
					str[i] = 'E';
					str[end] = 'E';
					
				}
				else
				{
					
					str[i] = '6';
					str[end] = '6';
				}
				end--;
				if (end <= i)
				{
					break;
				}
			}
			
			return new string(str);
		}
		//create random strings
		public string createString(int size)
		{
			
			char[] str = new char[size];
			
			int end = size - 1;
			int i = 1;
			for (i = 0; i < size; i++)
			{
				int x = UnityEngine.Random.Range(0, 3);
				if (x == 0)
				{
					int j = UnityEngine.Random.Range(0, 2);
					if (j == 0)
					{
						str[i] = 'X';
						str[end] = 'X';
					}
					else
					{
						str[i] = 'X';
						str[i + 1] = 'X';
						i++;
					}
				}
				else if (x == 1)
				{
					int j = UnityEngine.Random.Range(0, 2);
					if (j == 0)
					{
						str[i] = 'E';
						str[end] = 'E';
					}
					else
					{
						str[i] = 'E';
						str[i + 1] = 'E';
						i++;
					}
					
				}
				else
				{
					int j = UnityEngine.Random.Range(0, 2);
					if (j == 0)
					{
						str[i] = '6';
						str[end] = '6';
					}
					else
					{
						str[i] = '6';
						str[i + 1] = '6';
						i++;
					}
				}
				end--;
				if (end <= i)
				{
					break;
				}
			}
			
			return new string(str);
		}
		
		public bool checkPalindrome(string str)
		{
			int l = str.Length;
			for (int i = 0; i < l / 2; i++)
			{
				if (str[i] != str[l - 1 - i])
				{
					return false;
				}
			}
			return true;
			
		}
	}
	
}
