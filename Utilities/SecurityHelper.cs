using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LoginAppKatanaAPI
{
    public class SecurityHelper
    {
        public static string GenerateNonce(int size)
        {
            byte[] randomBytes = new byte[size];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }
        public static string EncodeBase64(string text)
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textBytes);
        }
        public static string HashData(string uid)
        {
            string rawData = $"{{\"challenge_nonce\":\"{GenerateNonce(32)}\",\"username\":\"{uid}\"}}";
            return EncodeBase64(rawData);
        }
        public static string GenerateVariable(AccountModel account)
        {
            var sharedGuid = Guid.NewGuid().ToString();
            var device_id = sharedGuid;
            var family_device_id = sharedGuid;

            var secure_family_device_id = Guid.NewGuid().ToString();
            var waterfall_id = Guid.NewGuid().ToString();
            var client_trace_id = Guid.NewGuid().ToString();

            string data = SecurityHelper.HashData(account.Uid!);

            string machine_id = StringHelper.GenerateRandomString(22);


            var variable = new
            {
                @params = new
                {
                    @params = JsonConvert.SerializeObject(new
                    {
                        client_input_params = new
                        {
                            sim_phones = Array.Empty<string>(),
                            secure_family_device_id = secure_family_device_id,
                            attestation_result = new
                            {
                                data = data,
                                signature = "MEYCIQDtz5TqO0pwysy82Ko92FErORasLag9o/pQYlZl8+zaMgIhAKon529upFiPfGgoS6OkPKg0/VahBuSDxwiTgtzpYQA3",
                                keyHash = "92398b3e4d9ee926bae93a61fd75e18d750100c1e73fd44d4faa7b9ba9353eee"
                            },
                            has_granted_read_contacts_permissions = 0,
                            auth_secure_device_id = "",
                            has_whatsapp_installed = 0,
                            password = StringHelper.GeneratePWDFormat(account.Password!),
                            sso_token_map_json_string = "",
                            event_flow = "login_manual",
                            password_contains_non_ascii = "false",
                            sim_serials = Array.Empty<string>(),
                            client_known_key_hash = "",
                            encrypted_msisdn = "",
                            has_granted_read_phone_permissions = 0,
                            app_manager_id = "null",
                            should_show_nested_nta_from_aymh = 0,
                            device_id = device_id,
                            login_attempt_count = 1,
                            machine_id = machine_id,
                            flash_call_permission_status = new
                            {
                                READ_PHONE_STATE = "DENIED",
                                READ_CALL_LOG = "DENIED",
                                ANSWER_PHONE_CALLS = "DENIED"
                            },
                            accounts_list = Array.Empty<string>(),
                            family_device_id = family_device_id,
                            fb_ig_device_id = Array.Empty<string>(),
                            device_emails = Array.Empty<string>(),
                            try_num = 2,
                            lois_settings = new { lois_token = "" },
                            event_step = "home_page",
                            headers_infra_flow_id = "",
                            openid_tokens = new { },
                            contact_point = account.Uid
                        },
                        server_params = new
                        {
                            should_trigger_override_login_2fa_action = 0,
                            is_from_logged_out = 0,
                            should_trigger_override_login_success_action = 0,
                            login_credential_type = "none",
                            server_login_source = "login",
                            waterfall_id = waterfall_id,
                            login_source = "Login",
                            is_platform_login = 0,
                            pw_encryption_try_count = 1,
                            INTERNAL__latency_qpl_marker_id = 36707139,
                            offline_experiment_group = "caa_iteration_v6_perf_fb_2",
                            is_from_landing_page = 0,
                            password_text_input_id = "jirv90:99",
                            is_from_empty_password = 0,
                            is_from_msplit_fallback = 0,
                            ar_event_source = "login_home_page",
                            username_text_input_id = "jirv90:98",
                            layered_homepage_experiment_group = (object)null,
                            device_id = device_id,
                            INTERNAL__latency_qpl_instance_id = 1.18039064400779E14,
                            reg_flow_source = "login_home_native_integration_point",
                            is_caa_perf_enabled = 1,
                            credential_type = "password",
                            is_from_password_entry_page = 0,
                            caller = "gslr",
                            family_device_id = family_device_id,
                            is_from_assistive_id = 0,
                            access_flow_version = "F2_FLOW",
                            is_from_logged_in_switcher = 0
                        }
                    }),
                    bloks_versioning_id = "cb6ac324faea83da28649a4d5046c3a4f0486cb987f8ab769765e316b075a76c",
                    app_id = "com.bloks.www.bloks.caa.login.async.send_login_request"
                },
                scale = "1.5",
                nt_context = new
                {
                    using_white_navbar = true,
                    styles_id = "55d2af294359fa6bbdb8e045ff01fc5e",
                    pixel_ratio = 1.5,
                    is_push_on = true,
                    debug_tooling_metadata_token = (object)null,
                    is_flipper_enabled = false,
                    theme_params = new object[] { },
                    bloks_version = "cb6ac324faea83da28649a4d5046c3a4f0486cb987f8ab769765e316b075a76c"
                }
            };

            string jsonVariable = JsonConvert.SerializeObject(variable);
            return jsonVariable;
        }
    }
}
