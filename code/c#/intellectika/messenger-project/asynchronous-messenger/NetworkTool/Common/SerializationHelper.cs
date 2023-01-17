namespace Common
{
	public static class SerializationHelper
	{
		public static MemoryStream Serialize<T>(T obj)
		{
			try
			{
				using (var ms = new MemoryStream())
				{
					ProtoBuf.Serializer.Serialize<T>(ms, obj);

					return ms;
				}
			}

			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		public static T Deserialize<T>(byte[] data)
		{
			try
			{
				T res = ProtoBuf.Serializer.Deserialize<T>(data.AsSpan());
				return res;
			}

			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}
	}
}
