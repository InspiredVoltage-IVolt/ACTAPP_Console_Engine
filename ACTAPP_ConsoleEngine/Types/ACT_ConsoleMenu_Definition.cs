using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ACT.Applications.ConsoleEngine.Types
{
	public class ACT_ConsoleMenu_Definition
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("display_name", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName { get; set; }

        [JsonProperty("startup_markupfile", NullValueHandling = NullValueHandling.Ignore)]
        public string StartupMarkupfile { get; set; }

        [JsonProperty("menu_header_markupfile", NullValueHandling = NullValueHandling.Ignore)]
        public string MenuHeaderMarkupfile { get; set; }

        [JsonProperty("menu_footer_markupfile", NullValueHandling = NullValueHandling.Ignore)]
        public string MenuFooterMarkupfile { get; set; }

        [JsonProperty("default_background", NullValueHandling = NullValueHandling.Ignore)]
        public string DefaultBackground { get; set; }

        [JsonProperty("default_foreground", NullValueHandling = NullValueHandling.Ignore)]
        public string DefaultForeground { get; set; }

        [JsonProperty("default_width", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? DefaultWidth { get; set; }

        [JsonProperty("default_height", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? DefaultHeight { get; set; }

        [JsonProperty("require_enter_button_press", NullValueHandling = NullValueHandling.Ignore)]
        public bool? RequireEnterButtonPress { get; set; }

        [JsonProperty("login_menu_keycode", NullValueHandling = NullValueHandling.Ignore)]
        public string LoginMenuKeycode { get; set; }

        [JsonProperty("login_menu_item_visible", NullValueHandling = NullValueHandling.Ignore)]
        public bool? LoginMenuItemVisible { get; set; }

        [JsonProperty("login_menu_position", NullValueHandling = NullValueHandling.Ignore)]
        public string LoginMenuPosition { get; set; }

        [JsonProperty("login_menu_show_on_all_menus", NullValueHandling = NullValueHandling.Ignore)]
        public bool? LoginMenuShowOnAllMenus { get; set; }

        [JsonProperty("login_menu_caption", NullValueHandling = NullValueHandling.Ignore)]
        public string LoginMenuCaption { get; set; }

        [JsonProperty("login_menu_display_markupfile", NullValueHandling = NullValueHandling.Ignore)]
        public string LoginMenuDisplayMarkupfile { get; set; }

        [JsonProperty("login_menu_method_name", NullValueHandling = NullValueHandling.Ignore)]
        public string LoginMenuMethodName { get; set; }

        [JsonProperty("admin_mode_keycode", NullValueHandling = NullValueHandling.Ignore)]
        public string AdminModeKeycode { get; set; }

        [JsonProperty("admin_mode_menu_item_visible", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AdminModeMenuItemVisible { get; set; }

        [JsonProperty("admin_mode_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? AdminModeId { get; set; }

        [JsonProperty("admin_mode_caption", NullValueHandling = NullValueHandling.Ignore)]
        public string AdminModeCaption { get; set; }

        [JsonProperty("admin_mode_show_on_all_menus", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AdminModeShowOnAllMenus { get; set; }

        [JsonProperty("admin_mode_position", NullValueHandling = NullValueHandling.Ignore)]
        public string AdminModePosition { get; set; }

        [JsonProperty("admin_mode_always_show_admin_header", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AdminModeAlwaysShowAdminHeader { get; set; }

        [JsonProperty("admin_header_display_markupfile", NullValueHandling = NullValueHandling.Ignore)]
        public string AdminHeaderDisplayMarkupfile { get; set; }

        [JsonProperty("admin_menu_option_markupfile", NullValueHandling = NullValueHandling.Ignore)]
        public string AdminMenuOptionMarkupfile { get; set; }

        [JsonProperty("permissions_file", NullValueHandling = NullValueHandling.Ignore)]
        public string PermissionsFile { get; set; }

        [JsonProperty("menu_items", NullValueHandling = NullValueHandling.Ignore)]
        public List<Item> MenuItems { get; set; }

        [JsonProperty("admin_menu_items", NullValueHandling = NullValueHandling.Ignore)]
        public List<Item> AdminMenuItems { get; set; }

        public static ACT_ConsoleMenu_Definition FromJson(string json) => JsonConvert.DeserializeObject<ACT_ConsoleMenu_Definition>(json, Converter.Settings);

        public string ToJson() => JsonConvert.SerializeObject(this, Converter.Settings);
    }

    public class Item
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("override_default")]
        public bool? OverrideDefault { get; set; }

        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        [JsonProperty("keycode", NullValueHandling = NullValueHandling.Ignore)]
        public string Keycode { get; set; }

        [JsonProperty("methodname", NullValueHandling = NullValueHandling.Ignore)]
        public string Methodname { get; set; }

        [JsonProperty("plugin", NullValueHandling = NullValueHandling.Ignore)]
        public string Plugin { get; set; }

        [JsonProperty("action_display_children", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ActionDisplayChildren { get; set; }

        [JsonProperty("execute", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Execute { get; set; }

        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<Item> Items { get; set; }
    }
    

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
